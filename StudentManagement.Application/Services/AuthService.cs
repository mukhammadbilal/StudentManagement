using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Models;
using StudentManagement.Infrastructure.Data;

namespace StudentManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly DbContext _context;

    public AuthService(DbContext context)
    {
        this._context = context;
    }

    public Teacher Authenticate(string username, string password)
    {
        return _context.Teachers.FirstOrDefault(t => t.Username == username && t.Password == password);
    }
}
