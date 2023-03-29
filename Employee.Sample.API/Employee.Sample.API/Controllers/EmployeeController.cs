using Employee.Sample.Data.DataModels;
using Employee.Sample.Data.ResultModels;
using Employee.Sample.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeService;
        public EmployeeController(IEmployee employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetEmployeeList([FromQuery]PagingData model)
        {
            IEnumerable<EmployeeInfo> result = new List<EmployeeInfo>();

            result = _employeeService.GetEmployeeList(model);

            return Ok(result);
        }

        [HttpGet]
        [Route("{name}")]
        public IActionResult GetEmployee(string name)
        {
            EmployeeInfo result = new EmployeeInfo();

            result = _employeeService.GetEmployeeByName(name);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult InsertEmployees(IFormFile? file, string? jsonString)
        {
            _employeeService.InsertEmployees(file, jsonString);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
