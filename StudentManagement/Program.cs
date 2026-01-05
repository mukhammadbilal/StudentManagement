using StudentManagement.Application.Services;
using StudentManagement.Domain.Models;

var studentService = new StudentService();

do
{
    ShowMenu();
    HandleMenuChoice();
}
while (AskToContinue());

void ShowMenu()
{
    Console.WriteLine();
    Console.WriteLine("-------------------------");
    Console.WriteLine("Student Management System");
    Console.WriteLine("-------------------------");
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. View All Students");
    Console.WriteLine("3. Get Student by Id");
    Console.WriteLine("4. Search by Name");
    Console.WriteLine("5. Update Student");
    Console.WriteLine("6. Delete Student");
}

void HandleMenuChoice()
{
    Console.Write("\nEnter your choice: ");

    if (int.TryParse(Console.ReadLine(), out int choice) == false)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid input. Please enter a number");
        Console.ResetColor();
        return;
    }

    switch (choice)
    {
        case 1:
            AddStudent();
            break;
        case 2:
            ViewAllStudents();
            break;
        case 3:
            GetStudentById();
            break;
        case 4:
            SearchStudentsByName();
            break;
        case 5:
            UpdateStudent();
            break;
        case 6:
            DeleteStudent();
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid choice. Please select a valid option from the menu");
            Console.ResetColor();
            break;
    }
}

bool AskToContinue()
{
    Console.WriteLine();
    Console.Write("Do you want to continue (y/n): ");

    string input = Console.ReadLine().ToLower();

    if (input == "y" || input == "yes")
    {
        return true;
    }
    else if (input == "n" || input == "no")
    {
        return false;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid input. Please enter 'y/yes' or 'n/no'");
        Console.ResetColor();
        return AskToContinue();
    }
}

void AddStudent()
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Adding a new student...");
    Console.ResetColor();

    Console.Write("\nEnter student first name: ");
    string firstName = Console.ReadLine();

    Console.Write("Enter student last name: ");
    string lastName = Console.ReadLine();

    Console.Write("Enter student code: ");
    string code = Console.ReadLine();

    Console.Write("Enter student email: ");
    string email = Console.ReadLine();

    var newStudent = new Student
    {
        Id = studentService.DbContext.Students.Any()
            ? studentService.DbContext.Students.Max(s => s.Id) + 1
            : 1,
        FirstName = firstName,
        LastName = lastName,
        Code = code,
        Email = email
    };

    studentService.AddStudent(newStudent);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\nStudent added successfully!");
    Console.ResetColor();
}

void ViewAllStudents()
{
    if (studentService.DbContext.Students.Any())
    {
        Console.WriteLine("\nAll students:");
        var students = studentService.GetAllStudents();

        foreach (var student in students)
        {
            studentService.PrintInfo(student);
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No students found. Try to add new students");
        Console.ResetColor();
    }
}

void GetStudentById()
{
    Console.WriteLine();
    Console.Write("Enter student ID: ");

    if (int.TryParse(Console.ReadLine(), out int id) == false)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid ID. Please enter a valid numeric ID");
        Console.ResetColor();
        return;
    }

    var student = studentService.GetStudentById(id);

    if (student == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Student not found");
        Console.ResetColor();
        return;
    }

    studentService.PrintInfo(student);
}

void SearchStudentsByName()
{
    Console.WriteLine();
    Console.Write("Enter search text: ");

    string searchText = Console.ReadLine();

    var students = studentService.SearchByText(searchText);

    if (students.Any())
    {
        Console.WriteLine("\nSearch results:");

        foreach (var student in students)
        {
            studentService.PrintInfo(student);
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No students found matching the search criteria");
        Console.ResetColor();
    }
}

void UpdateStudent()
{
    Console.WriteLine();
    Console.Write("Enter student ID to update: ");

    if (int.TryParse(Console.ReadLine(), out int id) == false)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid ID. Please enter a valid numeric ID");
        Console.ResetColor();
        return;
    }

    var existingStudent = studentService.GetStudentById(id);

    if (existingStudent == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Student not found");
        Console.ResetColor();
        return;
    }

    Console.Write("Change first name: ");
    string firstName = Console.ReadLine();

    Console.Write("Change last name: ");
    string lastName = Console.ReadLine();

    Console.Write("Update Student Code: ");
    string code = Console.ReadLine();

    Console.Write("Change email address: ");
    string emailAddress = Console.ReadLine();

    var student = new Student
    {
        Id = id,
        FirstName = firstName,
        LastName = lastName,
        Code = code,
        Email = emailAddress
    };

    studentService.UpdateStudent(student);
    Console.WriteLine("\nStudent updated successfully!");
}

void DeleteStudent()
{
    Console.WriteLine();
    Console.Write($"Enter student ID to delete: ");

    if (int.TryParse(Console.ReadLine(), out int id) == false)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid ID. Please enter a valid numeric ID");
        Console.ResetColor();
        return;
    }

    bool isDeleted = studentService.DeleteStudent(id);

    if (isDeleted)
    {
        Console.WriteLine("\nStudent deleted successfully!");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Student not found");
        Console.ResetColor();
    }
}