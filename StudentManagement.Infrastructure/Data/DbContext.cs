using StudentManagement.Domain.Models;

namespace StudentManagement.Infrastructure.Data;

public class DbContext
{
    public List<Student> Students { get; private set; }

    public DbContext()
    {
        this.Students = new List<Student>();

        SeedData();
    }

    private void SeedData()
    {
        if (Students.Any())
            return;

        Students.AddRange(new List<Student>
        {
            new Student { Id = 1, FirstName = "Alice", LastName = "Johnson", Code = "STU001", Email = "alice.johnson@example.com" },
            new Student { Id = 2, FirstName = "Bob", LastName = "Smith", Code = "STU002", Email = "bob.smith@example.com" },
            new Student { Id = 3, FirstName = "Carol", LastName = "Williams", Code = "STU003", Email = "carol.williams@example.com" },
            new Student { Id = 4, FirstName = "David", LastName = "Brown", Code = "STU004", Email = "david.brown@example.com" },
            new Student { Id = 5, FirstName = "Eva", LastName = "Davis", Code = "STU005", Email = "eva.davis@example.com" },
            new Student { Id = 6, FirstName = "Frank", LastName = "Miller", Code = "STU006", Email = "frank.miller@example.com" },
            new Student { Id = 7, FirstName = "Grace", LastName = "Wilson", Code = "STU007", Email = "grace.wilson@example.com" },
            new Student { Id = 8, FirstName = "Henry", LastName = "Moore", Code = "STU008", Email = "henry.moore@example.com" },
            new Student { Id = 9, FirstName = "Ivy", LastName = "Taylor", Code = "STU009", Email = "ivy.taylor@example.com" },
            new Student { Id = 10, FirstName = "Jack", LastName = "Anderson", Code = "STU010", Email = "jack.anderson@example.com" }
        });

    }
}
