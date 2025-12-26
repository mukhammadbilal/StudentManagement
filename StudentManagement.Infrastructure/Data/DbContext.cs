using StudentManagement.Domain.Models;

namespace StudentManagement.Infrastructure.Data;

public class DbContext
{
    public List<Student> Students { get; set; }
    public List<Attendance> Attendances { get; set; }

    public DbContext()
    {
        this.Students = new List<Student>();
        this.Attendances = new List<Attendance>();
    }
}
