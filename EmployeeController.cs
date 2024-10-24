using MappingByExtensionMethod.Filter;
using MappingByExtensionMethod.Response;
using MappingByExtensionMethod.Service;
using Microsoft.AspNetCore.Mvc;

namespace MappingByExtensionMethod;

[ApiController]
[Route("api/employee")]
public class EmployeeController(IEmployeeService employeeService):ControllerBase
{
    [HttpGet]
    public IActionResult GetEmployees([FromQuery] EmployeeFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<ReadEmployee>>>.Success(null,
            employeeService.GetAllEmployees(filter)));
    [HttpGet("{id:int}")]
    public IActionResult GetEmployeeById(int id)
    {
        ReadEmployee? res = employeeService.GetEmployeeById(id);
        return res != null
            ? Ok(ApiResponse<ReadEmployee?>.Success(null, res))
            : NotFound(ApiResponse<ReadEmployee?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreateEmployee([FromBody] string name,int age, string email, string phoneNumber)
    {
        CreateEmployee employee = new CreateEmployee(name, age, email,phoneNumber);
        bool res = employeeService.CreateEmployee(employee);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateEmployee(UpdateEmployee employee)
    {
        bool res = employeeService.UpdateEmployee(employee);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteEmployee(int id)
    {
        bool res = employeeService.DeleteEmployee(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}