using MediatR;

namespace EmployeeManagement.Library.Commands
{
    public sealed record AddEmployeeCommand(string FirstName, string LastName) : IRequest<EmployeeModel>;
}
