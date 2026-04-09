using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Interfaces;

public interface IAuthService
{
    Teacher Authenticate (string username, string password);
}
