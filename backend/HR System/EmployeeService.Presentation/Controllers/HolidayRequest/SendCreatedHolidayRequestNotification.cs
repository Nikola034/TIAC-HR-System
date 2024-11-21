using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using EmployeeService.Infrastructure.Services;
using FastEndpoints;

namespace EmployeeService.Presentation.Controllers.HolidayRequest;

public class SendCreatedHolidayRequestNotification : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("employees/holidayRequests/notifications/{userId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string userId = Route<string>("userId");

        // Create a channel for this user if it doesn't exist
        var userChannel = SseConnectionManager.UserConnections.GetOrAdd(userId, _ => Channel.CreateUnbounded<string>());

        // Set SSE headers
        HttpContext.Response.Headers.Add("Content-Type", "text/event-stream");
        HttpContext.Response.Headers.Add("Cache-Control", "no-cache");
        HttpContext.Response.Headers.Add("Connection", "keep-alive");

        try
        {
            var heartbeatTask = Task.Run(async () =>
            {
                while (!ct.IsCancellationRequested)
                {
                    await HttpContext.Response.WriteAsync(": heartbeat\n\n", ct); // Comment line for heartbeat
                    await HttpContext.Response.Body.FlushAsync(ct);
                    await Task.Delay(TimeSpan.FromSeconds(15), ct); // Adjust frequency as needed
                }
            }, ct);
            
            // Continuously read messages from the user's channel and send them
            while (!ct.IsCancellationRequested && await userChannel.Reader.WaitToReadAsync(ct))
            {
                while (userChannel.Reader.TryRead(out var message))
                {
                    await HttpContext.Response.WriteAsync($"data: {message}\n\n", Encoding.UTF8, ct);
                    await HttpContext.Response.Body.FlushAsync(ct);
                }
            }
        }
        finally
        {
            SseConnectionManager.UserConnections.TryRemove(userId, out _);
        }
    }
}