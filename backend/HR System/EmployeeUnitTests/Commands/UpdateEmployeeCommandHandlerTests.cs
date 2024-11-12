using Common.Exceptions;
using EmployeeService.Application.Commands.Employee;
using EmployeeService.Application.Common.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EmployeeUnitTests.Commands
{
    public class UpdateEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        public UpdateEmployeeCommandHandlerTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        }

        [Fact]
        public async Task Handle_Should_UpdateAndReturnUpdatedEmployee_WhenEmployeeExists()
        {
            // Arrange

            var command = new UpdateEmployeeCommand(

                Guid.Parse("3e41e111-f297-4b85-aad5-7e97b23d8a29"),
                "Rachel",
                "Doe",
                22,
                EmployeeService.Core.Enums.EmployeeRole.Developer
            );

            var domainEntity = new EmployeeService.Core.Entities.Employee
            {
                Id = Guid.Parse("3e41e111-f297-4b85-aad5-7e97b23d8a29"),
                Name = command.Name,
                Surname = command.Surname,
                DaysOff = command.DaysOff,
                Role = command.Role,
                AccountId = Guid.Parse("ae9071f9-1442-482d-8920-8e01b47d0c24")
            };

            _employeeRepositoryMock
                .Setup(repo => repo.CreateEmployeeAsync(It.IsAny<EmployeeService.Core.Entities.Employee>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(domainEntity);

            var handler = new UpdateEmployeeCommandHandler(
                _employeeRepositoryMock.Object
            );

            // Act

            var result = await handler.Handle(command, default);

            // Assert

            result.Should().NotBeNull();
            result.Should().BeOfType<EmployeeService.Core.Entities.Employee>();
            result.Id.Should().Be(command.Id);
            result.Name.Should().Be(command.Name);
            result.Surname.Should().Be(command.Surname);
        }

        [Fact]
        public async Task Handle_Should_ProjectNotFoundException_WhenProjectDoesNotExist()
        {
            // Arrange

            var command = new UpdateEmployeeCommand(
                Guid.Parse("c950f40b-0000-4493-a506-a13f952961d1"),
                "Radenko",
                "Salapura",
                20,
                EmployeeService.Core.Enums.EmployeeRole.Developer
            );

            var handler = new UpdateEmployeeCommandHandler(
                _employeeRepositoryMock.Object
            );

            // Art

            Func<Task> result = async () => await handler.Handle(command, default);

            // Assert

            await result.Should().ThrowAsync<NotFoundException>();
        }

    }
}
