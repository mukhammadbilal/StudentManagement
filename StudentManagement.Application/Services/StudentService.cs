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

    public Student GetStudentById(int id) =>
       this.DbContext.Students.FirstOrDefault(s => s.Id == id);

    public List<Student> SearchByText(string searchText) =>
        this.DbContext.Students
            .Where(s =>
            s.FirstName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            s.LastName.Contains(searchText, StringComparison.OrdinalIgnoreCase))
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

        this.DbContext.Students.Remove(student);
        return true;
    }

    public void PrintInfo(Student student)
    {
        Console.WriteLine($"Student Id: {student.Id}, Full Name: {student.FirstName} {student.LastName}, Code: {student.Code}, Email: {student.Email}");
    }
}
