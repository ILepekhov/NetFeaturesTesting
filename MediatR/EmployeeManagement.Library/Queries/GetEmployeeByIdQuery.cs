using MediatR;

namespace EmployeeManagement.Library.Queries
{
    public sealed record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeModel?>;
}
