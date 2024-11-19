using FastEndpoints;
using MediatR;
using ProjectServicePresentation.Contracts;
using ProjectServicePresentation.Mapper;

namespace ProjectServicePresentation.Controllers.Project;

public class AddEmployeeToProject : Endpoint<AddOrRemoveEmployeeFromProjectRequest, AddOrRemoveEmployeeFromProjectResponse>
{
    IMediator _mediator;

    public AddEmployeeToProject(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put("/projects/addToProject");
        Policies("ManagersOnly");
    }

    public override async Task HandleAsync(AddOrRemoveEmployeeFromProjectRequest req, CancellationToken ct)
    {
        var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
        var project = await _mediator.Send(req.ToAddCommand(authHeader), ct);
        await SendOkAsync(project.ToApiResponse(), ct);
    }
}

