namespace EmployeeManagement.Library
{
    public sealed class InMemoryDataAccess : IDataAccess
    {
        #region Fields

        private readonly List<EmployeeModel> _employees;

        #endregion

        #region Constructor

        public InMemoryDataAccess()
        {
            _employees = new()
            {
                new EmployeeModel { Id = 1, FirstName = "Praveen", LastName = "Raveendran Pillai" },
                new EmployeeModel { Id = 2, FirstName = "James", LastName = "roger" }
            };
        }

        #endregion

        #region IDataAccess

        public List<EmployeeModel> GetEmployees() => _employees.ToList();

        public EmployeeModel AddEmployee(string firstName, string lastName)
        {
            var id = _employees.Count == 0
                ? 1
                : _employees.Max(e => e.Id) + 1;

            var employee = new EmployeeModel()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            };

            _employees.Add(employee);

            return employee;
        }

        #endregion
    }
}
