using EmployeeManagement.Library.Queries;
using MediatR;

namespace EmployeeManagement.Library.Handlers
{
    public sealed class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeModel?>
    {
        #region Fields

        private readonly IDataAccess _dataAccess;

        #endregion

        #region Constructor

        public GetEmployeeByIdHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        #endregion

        #region Methods

        public Task<EmployeeModel?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dataAccess.GetEmployees().FirstOrDefault(x => x.Id == request.Id));
        }

        #endregion
    }
}
