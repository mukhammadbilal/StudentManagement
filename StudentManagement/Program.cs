using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Services;
using StudentManagement.Domain.Models;
using StudentManagement.Infrastructure.Data;
using StudentManagement.Presenters;

var dbContext = new DbContext();
IStudentService studentService = new StudentService(dbContext);
IAuthService authService = new AuthService(dbContext);

var loginUI = new LoginPresenter(authService);
var studentUI = new StudentPresenter(studentService);

loginUI.ShowMainMenu(studentUI);
