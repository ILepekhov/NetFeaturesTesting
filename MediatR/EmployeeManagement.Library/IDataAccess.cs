namespace EmployeeManagement.Library
{
    public interface IDataAccess
    {
        #region Methods

        List<EmployeeModel> GetEmployees();

        EmployeeModel AddEmployee(string firstName, string lastName);

        #endregion
    }
}
