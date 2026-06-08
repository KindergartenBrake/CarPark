using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CP.Server.DTO;
using CP.Server.Data;
using CP.Server.Models;
using CP.Server.Services.Interfaces;
namespace CP.Server.Services;


public class EmployeeService : IEmployeeService
{
    private readonly CarParkContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public EmployeeService(CarParkContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<List<EmployeeDto>> GetAllEmployeesAsync(string? search = null)
    {
        // Получаем всех пользователей через UserManager
        var users = _userManager.Users.ToList();
        
        // Фильтруем пользователей, у которых есть заявки
        var usersWithRequests = users.Where(u => _context.TripRequests.Any(r => r.UserId == u.Id)).ToList();

        if (!string.IsNullOrWhiteSpace(search))
        {
            usersWithRequests = usersWithRequests.Where(u => 
                (u.UserName != null && u.UserName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                (u.Email != null && u.Email.Contains(search, StringComparison.OrdinalIgnoreCase))).ToList();
        }

        var result = new List<EmployeeDto>();

        foreach (var user in usersWithRequests)
        {
            var requests = await _context.TripRequests
                .Where(r => r.UserId == user.Id)
                .ToListAsync();

            result.Add(new EmployeeDto
            {
                UserId = user.Id,
                FullName = user.UserName ?? user.Email ?? "—",
                Email = user.Email ?? "—",
                TotalRequests = requests.Count,
                ActiveRequests = requests.Count(r => r.Status == "Pending" || r.Status == "Approved"),
                IsActive = !(user.LockoutEnabled && user.LockoutEnd > DateTime.UtcNow),
                Requests = requests
                    .OrderByDescending(r => r.CreatedAt)
                    .Take(5)
                    .Select(r => new EmployeeRequestDto
                    {
                        RequestId = r.RequestId,
                        Route = r.Description ?? $"Поездка {r.TripDate:dd.MM.yyyy}",
                        IsActive = r.Status == "Pending" || r.Status == "Approved",
                        TripDate = r.TripDate,
                        Status = r.Status ?? "Pending"
                    }).ToList()
            });
        }

        return result;
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return null;

        var requests = await _context.TripRequests
            .Where(r => r.UserId == userId)
            .ToListAsync();

        return new EmployeeDto
        {
            UserId = user.Id,
            FullName = user.UserName ?? user.Email ?? "—",
            Email = user.Email ?? "—",
            TotalRequests = requests.Count,
            ActiveRequests = requests.Count(r => r.Status == "Pending" || r.Status == "Approved"),
            IsActive = !(user.LockoutEnabled && user.LockoutEnd > DateTime.UtcNow),
            Requests = requests
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new EmployeeRequestDto
                {
                    RequestId = r.RequestId,
                    Route = r.Description ?? $"Поездка {r.TripDate:dd.MM.yyyy}",
                    IsActive = r.Status == "Pending" || r.Status == "Approved",
                    TripDate = r.TripDate,
                    Status = r.Status ?? "Pending"
                }).ToList()
        };
    }

    public async Task DeactivateEmployeeAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            // Блокируем пользователя
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTime.UtcNow.AddYears(100);
            await _userManager.UpdateAsync(user);
        }
    }

    public async Task ActivateAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return;
        
        // Разблокируем пользователя
        user.LockoutEnabled = false;
        user.LockoutEnd = null;
        await _userManager.UpdateAsync(user);
    }
}