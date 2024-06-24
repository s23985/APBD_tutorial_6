using APBD_tutorial_6.Models;
using APBD_tutorial_6.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_tutorial_6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;
    
    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }
    
    [HttpPost]
    public IActionResult AddProductToWarehouse([FromBody] ProductRequest request)
    {
        try
        {
            var newId = _warehouseService.AddProductToWarehouse(request);
            return Ok(newId);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}