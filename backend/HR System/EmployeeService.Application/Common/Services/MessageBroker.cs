using System.Collections.Concurrent;
using System.Threading.Channels;

namespace EmployeeService.Infrastructure.Services;

public static class SseConnectionManager
{
    public static readonly ConcurrentDictionary<string, Channel<string>> UserConnections = new();
}