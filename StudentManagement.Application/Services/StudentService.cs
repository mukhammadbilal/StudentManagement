using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Models;
using StudentManagement.Infrastructure.Data;

namespace StudentManagement.Application.Services;

public class StudentService : IStudentService
{
    private readonly DbContext _context;

    public StudentService(DbContext context)
    {
        _context = context;
    }

    public void AddStudent(Student student)
    {
        student.Id = _context.Students.Any() ? _context.Students.Max(s => s.Id) + 1 : 1;
        _context.Students.Add(student);
    }

    public List<Student> GetAllStudents() =>
        _context.Students;

    public Student GetStudentById(int id) =>
       _context.Students.FirstOrDefault(s => s.Id == id);

    public List<Student> SearchByText(string text) =>
        _context.Students
            .Where(s =>
                s.FirstName.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                s.LastName.Contains(text, StringComparison.OrdinalIgnoreCase))
            .ToList();

    public bool UpdateStudent(Student student)
    {
        var existingStudent = GetStudentById(student.Id);

        if (existingStudent == null)
        {
            return false;
        }

        existingStudent.FirstName = student.FirstName;
        existingStudent.LastName = student.LastName;
        existingStudent.Code = student.Code;
        existingStudent.Email = student.Email;

        return true;
    }

    public bool DeleteStudent(int id)
    {
        var student = GetStudentById(id);

        if (student == null)
        {
            return false;
        }

        _context.Students.Remove(student);
        return true;
    }
}
