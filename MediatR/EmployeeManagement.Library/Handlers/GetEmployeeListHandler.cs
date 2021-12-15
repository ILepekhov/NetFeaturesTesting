using EmployeeManagement.Library.Queries;
using MediatR;

namespace EmployeeManagement.Library.Handlers
{
    public sealed class GetEmployeeListHandler : IRequestHandler<GetEmployeeListQuery, List<EmployeeModel>>
    {
        #region Fields

        private readonly IDataAccess _dataAccess;

        #endregion

        #region Constructor

        public GetEmployeeListHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        #endregion

        #region Methods

        public Task<List<EmployeeModel>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dataAccess.GetEmployees());
        }

        #endregion
    }
}
