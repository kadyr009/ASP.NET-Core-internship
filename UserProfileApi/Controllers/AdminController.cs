using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserProfileApi.Data;
using UserProfileApi.Models;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("add-field")]
    [SwaggerOperation("CreateField")]
    [SwaggerResponse(200, "Field created successfully")]
    public async Task<ActionResult> CreateField([FromBody] ProfileField field)
    {
        _context.ProfileFields.Add(field);
        await _context.SaveChangesAsync();
        return Ok("Field created successfully");
    }

    [HttpGet("fields")]
    [SwaggerOperation("GetFields")]
    public async Task<ActionResult<IEnumerable<ProfileField>>> GetFields()
    {
        return await _context.ProfileFields.ToListAsync();
    }
}