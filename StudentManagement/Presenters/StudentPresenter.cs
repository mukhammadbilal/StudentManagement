using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Presenters;

public class StudentPresenter
{
    private readonly IStudentService _studentService;

    public StudentPresenter(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public void ShowStudentMenu()
    {
        bool continueRunning = true;
        while (continueRunning)
        {
            Console.Clear();
            Console.WriteLine("-------------------------");
            Console.WriteLine("Student Management System");
            Console.WriteLine("-------------------------");
            Console.WriteLine("1. Add Student\n2. View All\n3. Get by Id\n4. Search\n5. Update\n6. Delete\n0. Back to Login page");

            Console.Write("\nSelect option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": CreateStudentView(); break;
                case "2": ListStudentsView(); break;
                case "3": GetByIdView(); break;
                case "4": SearchView(); break;
                case "5": UpdateView(); break;
                case "6": DeleteView(); break;
                case "0": continueRunning = false; break;
                default: ErrorMessage("\nInvalid option! Please select between 0 and 6"); break;
            }

            if (continueRunning)
            {
                AskToContinue();
            }
        }
    }

    private void CreateStudentView()
    {
        Console.WriteLine("\n--- Add Student ---");
        var student = new Student
        {
            FirstName = ReadInput("First Name: "),
            LastName = ReadInput("Last Name: "),
            Code = ReadInput("Code: "),
            Email = ReadInput("Email: ")
        };

        _studentService.AddStudent(student);
        SuccessMessage("Student added successfully!");
    }

    private void ListStudentsView()
    {
        var students = _studentService.GetAllStudents();
        if (!students.Any())
        {
            Console.WriteLine("No students found");
            return;
        }

        foreach (var student in students)
        {
            PrintStudent(student);
        }
    }
    private void GetByIdView()
    {
        if (!int.TryParse(ReadInput("Enter Id: "), out int id))
        {
            Console.WriteLine("\nInvalid ID. Please enter a valid numeric ID");
            return;
        }

        var student = _studentService.GetStudentById(id);

        if (student == null)
        {
            Console.WriteLine($"\nStudent not found");
            return;
        }

        Console.WriteLine();
        PrintStudent(student);
    }

    private void SearchView()
    {
        var text = ReadInput("Enter search text: ");

        var students = _studentService.SearchByText(text);

        if (!students.Any())
        {
            Console.WriteLine("\nNo students found matching the search criteria");
        }

        foreach (var student in students)
        {
            PrintStudent(student);
        }
    }
    private void UpdateView()
    {
        if (!int.TryParse(ReadInput("Enter student ID to update: "), out int id))
        {
            Console.WriteLine("\nInvalid ID. Please enter a valid numeric ID");
            return;
        }

        var existingStudent = _studentService.GetStudentById(id);

        if (existingStudent == null)
        {
            Console.WriteLine($"\nStudent not found");
            return;
        }

        string firstName = ReadInput("Change first name: ");
        string lastName = ReadInput("Change last name: ");
        string code = ReadInput("Update Student Code: ");
        string emailAddress = ReadInput("Change email address: ");

        var student = new Student
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Code = code,
            Email = emailAddress
        };

        _studentService.UpdateStudent(student);
        Console.WriteLine("\nStudent updated successfully!");
    }

    private void DeleteView()
    {
        if (!int.TryParse(ReadInput("Enter student ID to delete: "), out int id))
        {
            Console.WriteLine("\nInvalid ID. Please enter a valid numeric ID");
            return;
        }

        var existingStudent = _studentService.GetStudentById(id);

        if (existingStudent == null)
        {
            Console.WriteLine($"\nStudent not found");
            return;
        }

        _studentService.DeleteStudent(id);
        Console.WriteLine("\nDeleted successully");
    }

    private static string ReadInput(string label)
    {
        Console.Write(label);

        return Console.ReadLine();
    }

    private static void PrintStudent(Student s) =>
        Console.WriteLine($"[ID: {s.Id}] {s.FirstName} {s.LastName} | Code: {s.Code} | Email: {s.Email}");

    private static void SuccessMessage(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.WriteLine(msg);
        Console.ResetColor();
    }

    private static void ErrorMessage(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(msg); Console.ResetColor();
    }

    private static void AskToContinue()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}
