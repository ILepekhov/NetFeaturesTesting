namespace EmployeeManagement.Library
{
    public sealed class EmployeeModel
    {
        #region Properties

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        #endregion

        #region Constructor

        public EmployeeModel()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        #endregion
    }
}
