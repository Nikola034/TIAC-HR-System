using Common.HttpCLients;
using EmployeeService.Application.Commands.Employee;
using EmployeeService.Application.Common.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeUnitTests.Commands
{
    public class DeleteEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IHolidayRequestApproverRepository> _holidayRequestRepositoryMock;
        private readonly Mock<IHolidayRequestRepository> _holidayRequestApproverRepositoryMock;
        private readonly Mock<IAccountServiceHttpClient> _accountRepositoryMock;
        private readonly Mock<IProjectHttpClient> _projectRepositoryMock;


        public DeleteEmployeeCommandHandlerTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _holidayRequestApproverRepositoryMock = new Mock<IHolidayRequestRepository>();
            _holidayRequestRepositoryMock = new Mock<IHolidayRequestApproverRepository>();
            _accountRepositoryMock = new Mock<IAccountServiceHttpClient>();
            _projectRepositoryMock = new Mock<IProjectHttpClient>();
        }

        [Fact]
        public async Task Handle_Should_DeleteProject_WhenProjectExists()
        {
            // Arrange

            var command = new DeleteEmployeeCommand(
                Guid.Parse("c950f40b-1480-4493-a506-a13f952961d1")
            );

            var taskIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            var employeeToDelete = new EmployeeService.Core.Entities.Employee
            {
                Id = Guid.Parse("c950f40b-1480-4493-a506-a13f952961d1"),
                Name = "Radenko",
                Surname = "Salapura",
                DaysOff = 20,
                Role = EmployeeService.Core.Enums.EmployeeRole.Developer,
                AccountId = Guid.Parse("d97fc3c4-871f-49cf-99dd-8f673d4cb194")
            };

            var domainEntity = new EmployeeService.Core.Entities.Employee
            {
                Id = Guid.Parse("c950f40b-1480-4493-a506-a13f952961d1"),
                Name = "Radenko",
                Surname = "Salapura",
                DaysOff = 20,
                Role = EmployeeService.Core.Enums.EmployeeRole.Developer,
                AccountId = Guid.Parse("d97fc3c4-871f-49cf-99dd-8f673d4cb194")
            };
            _employeeRepositoryMock
               .Setup(repo => repo.CreateEmployeeAsync(It.IsAny<EmployeeService.Core.Entities.Employee>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(domainEntity);

            var handler = new DeleteEmployeeCommandHandler(
                _employeeRepositoryMock.Object,
                _holidayRequestApproverRepositoryMock.Object,
                _holidayRequestRepositoryMock.Object,
                _accountRepositoryMock.Object,
                _projectRepositoryMock.Object
            );

            // Act

            var result = await handler.Handle(command, CancellationToken.None);

            // Assert

            result.Should().BeTrue();
        }

    }
}
