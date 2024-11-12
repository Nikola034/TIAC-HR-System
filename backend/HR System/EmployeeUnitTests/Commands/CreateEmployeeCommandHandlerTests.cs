using EmployeeService.Application.Commands.Employee;
using EmployeeService.Application.Common.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace EmployeeUnitTests.Commands
{
    public class CreateEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;

        public CreateEmployeeCommandHandlerTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        }

        [Fact]
        public async Task Handle_Should_CreateAndReturnProject()
        {
            // Arrange

            var command = new CreateEmployeeCommand("Radenko", "Salapura", 20, EmployeeService.Core.Enums.EmployeeRole.Developer, Guid.Parse("d97fc3c4-871f-49cf-99dd-8f673d4cb194"));

            var domainEntity = new EmployeeService.Core.Entities.Employee
            {
                Name = command.Name,
                Surname = command.Surname,
                DaysOff = command.DaysOff,
                Role = command.Role,
                AccountId = command.AccountId
            };

            _employeeRepositoryMock
                .Setup(repo => repo.CreateEmployeeAsync(It.IsAny<EmployeeService.Core.Entities.Employee>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(domainEntity);

            var handler = new CreateEmployeeCommandHandler(
                _employeeRepositoryMock.Object
            );

            // Act

            var result = await handler.Handle(command, default);

            // Assert

            result.Should().NotBeNull();
            result.Should().BeOfType<EmployeeService.Core.Entities.Employee>();
        }

    }
}
