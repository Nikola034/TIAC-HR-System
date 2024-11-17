using Application.Commands;
using Application.Commands.Project;
using Application.Common.Repositories;
using Core.Entities;
using Core.Exceptions;
using Moq;

namespace ProjectUnitTests;

public class DeleteCommandTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly Mock<IEmployeeProjectRepository> _employeeProjectRepositoryMock;

    public DeleteCommandTests()
    {
        _projectRepositoryMock = new();
        _employeeProjectRepositoryMock = new();
    }
    
    [Fact]
    public async Task Handle_Should_ReturnFalseResult_WhenIdIsNotFound()
    {
        // Arrange
        var command = new DeleteProjectCommand(new Guid());

        _projectRepositoryMock.Setup(
                x => x.DeleteProjectAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        
        var handler = new DeleteProjectCommandHandler(_projectRepositoryMock.Object,_employeeProjectRepositoryMock.Object);
        // Act
        var result = await handler.Handle(command, default);
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnTrueResult_WhenIdIsFound()
    {
        // Arrange
        var command = new DeleteProjectCommand(new Guid("5ff2ab92-1673-45bb-b70b-e36620fcd829"));

        _projectRepositoryMock.Setup(
                x => x.DeleteProjectAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        
        var handler = new DeleteProjectCommandHandler(_projectRepositoryMock.Object,_employeeProjectRepositoryMock.Object);
        // Act
        var result = await handler.Handle(command, default);
        // Assert
        Assert.True(result);
    }
}