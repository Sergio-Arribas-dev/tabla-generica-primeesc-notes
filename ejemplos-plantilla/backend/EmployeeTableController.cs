using ECS.PrimengTable.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Tables.Employees;

[ApiController]
[Route("api/employee-table")]
public sealed class EmployeeTableController : ControllerBase
{
    private readonly IEmployeeTableService _service;

    public EmployeeTableController(IEmployeeTableService service) => _service = service;

    [HttpGet("configuration")]
    public ActionResult<TableConfigurationModel> GetConfiguration()
    {
        return Ok(_service.GetTableConfiguration());
    }

    [HttpPost("data")]
    public IActionResult GetData([FromBody] TableQueryRequestModel request)
    {
        var result = _service.GetTableData(request);
        if (!result.success)
            return BadRequest(new { message = result.error });
        return Ok(result.data);
    }
}
