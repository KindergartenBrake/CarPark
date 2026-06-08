using System.Security.Claims;
using CP.Server.Models;
using CP.Server.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CP.Server.Data;
using CP.Server.Services.Interfaces;
namespace CP.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly CarParkContext _context;

    public ProfileController(
        UserManager<ApplicationUser> userManager,
        CarParkContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    // GET: api/profile (для всех пользователей)
    [HttpGet]
    public async Task<ActionResult<ProfileDto>> Get()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        return new ProfileDto
        {
            Id = user.Id,
            UserName = user.UserName ?? "",
            Email = user.Email ?? "",
            PhoneNumber = user.PhoneNumber ?? ""
        };
    }

    // PUT: api/profile
    [HttpPut]
    public async Task<IActionResult> Update(ProfileDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        user.UserName = dto.UserName;
        user.Email = dto.Email;
        user.PhoneNumber = dto.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok();
    }

    // POST: api/profile/change-password
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok();
    }

    // GET: api/profile/driver (для водителя)
    [HttpGet("driver")]
    public async Task<ActionResult<DriverProfileDto>> GetDriverProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.UserId == userId);

        return new DriverProfileDto
        {
            FullName = driver != null 
                ? $"{driver.LastName} {driver.FirstName} {driver.MiddleName}".Trim()
                : user.UserName ?? user.Email ?? "",
            Email = user.Email ?? "",
            Phone = driver?.Phone ?? user.PhoneNumber ?? "",
            DriverLicenseNumber = driver?.LicenseNumber ?? "",
            LicenseExpiration = driver?.LicenseExpiryDate ?? DateTime.UtcNow.AddYears(3),
            IsActive = driver?.IsActive ?? true
        };
    }

    // PUT: api/profile/driver (для водителя)
    [HttpPut("driver")]
    public async Task<IActionResult> UpdateDriverProfile(DriverProfileDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        // Обновляем телефон
        if (!string.IsNullOrEmpty(dto.Phone))
        {
            user.PhoneNumber = dto.Phone;
            await _userManager.UpdateAsync(user);
        }

        // Обновляем данные водителя
        var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.UserId == userId);
        if (driver != null)
        {
            driver.Phone = dto.Phone;
            driver.LicenseNumber = dto.DriverLicenseNumber;
            driver.LicenseExpiryDate = dto.LicenseExpiration;
            await _context.SaveChangesAsync();
        }

        return Ok();
    }
}