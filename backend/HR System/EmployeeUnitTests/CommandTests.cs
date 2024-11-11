using Common.HttpCLients;
using EmployeeService.Application.Commands.Employee;
using EmployeeService.Application.Common.Repositories;
using EmployeeService.Core.Entities;
using EmployeeService.Core.Primitives.Result;
using Moq;

namespace EmployeeUnitTests
{
    public class CommandTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IHolidayRequestRepository> _holidayRequestRepositoryMock;
        private readonly Mock<IHolidayRequestApproverRepository> _holidayRequestApproverRepositoryMock;
        private readonly Mock<IAccountServiceHttpClient> _accountRepositoryMock;
        private readonly Mock<IProjectHttpClient> _projectRepositoryMock;

        public CommandTests()
        {
            _employeeRepositoryMock = new();
            _holidayRequestRepositoryMock = new();
            _holidayRequestApproverRepositoryMock = new();
            _accountRepositoryMock = new();
            _projectRepositoryMock = new();
        }

        [Fact]
        public async void Handle_Should_ReturnFailureResult_WhenEmployeeDoesntExist()
        {
            // Arrange
            var command = new DeleteEmployeeCommand(Guid.Parse("127e3434-d303-4ebe-8109-f35b848b6f0f"));
            var handler = new DeleteEmployeeCommandHandler(_employeeRepositoryMock.Object, _holidayRequestRepositoryMock.Object, _holidayRequestApproverRepositoryMock.Object,
                _accountRepositoryMock.Object, _projectRepositoryMock.Object);
            // Act
            Result<Employee> result = await handler.Handle(command, default);
            // Assert
            await Task.FromResult(result);
        }
    }
}