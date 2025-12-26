using StudentManagement.Domain.Models;
using StudentManagement.Infrastructure.Data;

namespace StudentManagement.Application.Services;

public class StudentService
{
    public DbContext DbContext { get; set; }

    public StudentService()
    {
        this.DbContext = new DbContext();
    }

    public void AddStudent(Student student)
    {
        this.DbContext.Students.Add(student);
    }

    public List<Student> GetAllStudents() =>
        this.DbContext.Students;

    public void PrintInfo(Student student)
    {
        Console.WriteLine($"Student Id: {student.Id}, Full Name: {student.FirstName} {student.LastName}, Code: {student.Code}, Email: {student.Email}");
    }
}
