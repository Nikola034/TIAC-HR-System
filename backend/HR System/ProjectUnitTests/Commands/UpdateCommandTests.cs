using Application.Commands.Project;
using Application.Common.Repositories;
using Core.Entities;
using Core.Exceptions;
using Moq;

namespace ProjectUnitTests;

public class UpdateCommandTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;

    public UpdateCommandTests()
    {
        _projectRepositoryMock = new();
    }
    
    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenIdIsNotFound()
    {
        // Arrange
        var command = new UpdateProjectCommand(
            new Guid(), "An old project", "Some description", new Guid(), new Guid());

        _projectRepositoryMock.Setup(
                x => x.GetProjectByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync((Project?)null);
        
        var handler = new UpdateProjectCommandHandler(_projectRepositoryMock.Object);
        // Act
        // Assert
        await Assert.ThrowsAsync<ProjectDoesNotExistException>(() => handler.Handle(command,default));
    }
    
    [Fact]
    public async Task Handle_Should_ReturnUpdatedResult_WhenIdIsFound()
    {
        // Arrange
        var command = new UpdateProjectCommand(
            new Guid("a0911106-8a11-4f1f-b441-a5657f0f160d"), "An old project", "Some description", new Guid(), new Guid());

        _projectRepositoryMock.Setup(
                x => x.GetProjectByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Project());
        
        var handler = new UpdateProjectCommandHandler(_projectRepositoryMock.Object);
        // Act
        var updatedProject = handler.Handle(command, default);
        // Assert
        Assert.True(updatedProject != null);
    }
}