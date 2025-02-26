using Microsoft.AspNetCore.Mvc;
using UserProfileApi.Data;
using UserProfileApi.Models;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/[controller]")]
[ApiController]
public class UserProfileController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserProfileController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [SwaggerOperation("CreateProfile")]
    [SwaggerResponse(200, "Profile created successfully")]
    public async Task<ActionResult> CreateProfile([FromBody] UserProfile profile)
    {
        _context.UserProfiles.Add(profile);
        await _context.SaveChangesAsync();
        return Ok("Profile created successfully");
    }

    [HttpGet("{id}")]
    [SwaggerOperation("GetProfile")]
    public async Task<ActionResult<UserProfile>> GetProfile(int id)
    {
        var profile = await _context.UserProfiles.FindAsync(id);
        
        if (profile == null) return NotFound();

        return profile;
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation("UpdateProfile")]
    [SwaggerResponse(200, "Profile updated successfully")]
    public async Task<ActionResult> UpdateProfile(int id, [FromBody] UserProfile updatedProfile)
    {
        var profile = await _context.UserProfiles.FindAsync(id);
        
        if (profile == null) return NotFound();

        profile.Nickname = updatedProfile.Nickname;
        profile.AvatarUrl = updatedProfile.AvatarUrl;
        profile.ExpertiseAreas = updatedProfile.ExpertiseAreas;
        profile.Gender = updatedProfile.Gender;
        profile.BirthDate = updatedProfile.BirthDate;
        profile.BirthTime = updatedProfile.BirthTime;
        profile.BirthPlace = updatedProfile.BirthPlace;
        profile.CurrentLocation = updatedProfile.CurrentLocation;

        await _context.SaveChangesAsync();
        return Ok("Profile updated successfully");
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("DeleteProfile")]
    [SwaggerResponse(200, "Profile deleted successfully")]
    public async Task<ActionResult> DeleteProfile(int id)
    {
        var profile = await _context.UserProfiles.FindAsync(id);
        
        if (profile == null) return NotFound();

        _context.UserProfiles.Remove(profile);
        await _context.SaveChangesAsync();
        return Ok("Profile deleted successfully");
    }
    
    [HttpPost("{id}/upload-avatar")]
    [SwaggerOperation("UploadAvatar")]
    [SwaggerResponse(200, "Avatar uploaded successfully")]
    public async Task<ActionResult> UploadAvatar(int id, IFormFile file)
    {
        var profile = await _context.UserProfiles.FindAsync(id);
        
        if (profile == null) return NotFound();

        if(file.Length > 100 * 1024) return BadRequest("File size should be less than 100KB");

        var directoryPath = Path.Combine("wwwroot", "avatars");

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);


        var filePath = Path.Combine("wwwroot", "avatars", file.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        profile.AvatarUrl = $"/avatars/{file.FileName}";
        await _context.SaveChangesAsync();

        return Ok("Avatar uploaded successfully");
    }
}