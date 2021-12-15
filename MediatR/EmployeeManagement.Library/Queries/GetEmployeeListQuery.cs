using MediatR;

namespace EmployeeManagement.Library.Queries
{
    public sealed record GetEmployeeListQuery() : IRequest<List<EmployeeModel>>;
}
