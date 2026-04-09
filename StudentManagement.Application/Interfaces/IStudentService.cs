using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Interfaces;

public interface IStudentService
{
    public void AddStudent(Student student);
    public List<Student> GetAllStudents();
    public Student GetStudentById(int id);
    public List<Student> SearchByText(string text);
    public bool UpdateStudent(Student student);
    public bool DeleteStudent(int id);
}
