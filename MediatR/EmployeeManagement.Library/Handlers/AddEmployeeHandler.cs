using EmployeeManagement.Library.Commands;
using MediatR;

namespace EmployeeManagement.Library.Handlers
{
    public sealed class AddEmployeeHandler : IRequestHandler<AddEmployeeCommand, EmployeeModel>
    {
        #region Fields

        private readonly IDataAccess _dataAccess;

        #endregion

        #region Constructor

        public AddEmployeeHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        #endregion

        #region Methods

        public Task<EmployeeModel> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_dataAccess.AddEmployee(request.FirstName, request.LastName));
        }

        #endregion
    }
}
