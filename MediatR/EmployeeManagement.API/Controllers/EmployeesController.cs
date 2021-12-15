using EmployeeManagement.Library;
using EmployeeManagement.Library.Commands;
using EmployeeManagement.Library.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Constructor

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region Endpoints

        [HttpGet]
        public async Task<List<EmployeeModel>> Get()
        {
            return await _mediator.Send(new GetEmployeeListQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeModel?>> Get(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));

            if (employee is null)
            {
                return NotFound(id);
            }

            return employee;
        }

        [HttpPost]
        public async Task<EmployeeModel> Post([FromBody] EmployeeModel employee)
        {
            return await _mediator.Send(new AddEmployeeCommand(employee.FirstName, employee.LastName));
        }

        #endregion
    }
}
