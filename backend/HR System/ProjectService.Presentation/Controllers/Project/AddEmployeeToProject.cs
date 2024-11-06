using FastEndpoints;
using MediatR;
using ProjectServicePresentation.Contracts;
using ProjectServicePresentation.Mapper;

namespace ProjectServicePresentation.Controllers.Project;

public class AddEmployeeToProject : Endpoint<AddEmployeeToProjectRequest, AddEmployeeToProjectResponse>
{
    IMediator _mediator;

    public AddEmployeeToProject(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/projects/addToProject");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddEmployeeToProjectRequest req, CancellationToken ct)
    {
        var project = await _mediator.Send(req.ToCommand(), ct);
        await SendOkAsync(project.ToApiResponseFromAdd(), ct);
    }
}

