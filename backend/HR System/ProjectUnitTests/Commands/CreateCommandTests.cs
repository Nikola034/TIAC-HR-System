using Application.Common.Repositories;
using Core.Entities;
using Moq;
using ProjectServiceApplication.Commands.Project;

namespace ProjectUnitTests;

public class CreateCommandTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly Mock<IEmployeeProjectRepository> _employeeProjectRepositoryMock;

    public CreateCommandTests()
    {
        _projectRepositoryMock = new();
        _employeeProjectRepositoryMock = new();
    }

    [Fact]
    public async Task Handle_ShouldNot_CallEmployeeProjectRepository_WhenTeamLeadIdIsNull()
    {
        //Arrange
        var command = new CreateProjectCommand(
             "An old project", "Some description", new Guid(), null);

        _projectRepositoryMock.Setup(
                x => x.CreateProjectAsync(
                    It.IsAny<Project>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Project());

        var createHandler = new CreateProjectCommandHandler(_projectRepositoryMock.Object, _employeeProjectRepositoryMock.Object);
        //Act
        await createHandler.Handle(command, default);
        //Assert
        _employeeProjectRepositoryMock.Verify(x => x.AddEmployeeToProjectAsync(It.IsAny<EmployeeProject>(),default),Times.Never());

    }
    
    [Fact]
    public async Task Handle_Should_CallEmployeeProjectRepository_WhenTeamLeadIdIsNotNull()
    {
        //Arrange
        var command = new CreateProjectCommand(
            "An old project", "Some description", new Guid(), Guid.Parse("5ff2ab92-1673-45bb-b70b-e36620fcd829"));

        _projectRepositoryMock.Setup(
                x => x.CreateProjectAsync(
                    It.IsAny<Project>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Project
            {
                Id = Guid.Parse("a0911106-8a11-4f1f-b441-a5657f0f160d"),
                Title = "An old project",
                Description = "Some description",
                Client = new Client(),
                ClientId = new Guid(),
                TeamLeadId = Guid.Parse("5ff2ab92-1673-45bb-b70b-e36620fcd829")
            });
        var employeeProject = new EmployeeProject
        {
            ProjectId = Guid.Parse("a0911106-8a11-4f1f-b441-a5657f0f160d"),
            EmployeeId = Guid.Parse("5ff2ab92-1673-45bb-b70b-e36620fcd829")
        };
        var createHandler = new CreateProjectCommandHandler(_projectRepositoryMock.Object, _employeeProjectRepositoryMock.Object);
        //Act
        await createHandler.Handle(command, default);
        //Assert
        _employeeProjectRepositoryMock.Verify(
            x => x.AddEmployeeToProjectAsync(It.Is<EmployeeProject>(
                ep => ep.EmployeeId == employeeProject.EmployeeId && ep.ProjectId == employeeProject.ProjectId),default),Times.Once());

    }
}