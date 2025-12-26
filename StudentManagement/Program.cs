using StudentManagement.Application.Services;
using StudentManagement.Domain.Models;

bool continueRunning = true;
var studentService = new StudentService();

do
{
    ShowMenu();
    
    Console.Write("\nEnter your choice: ");
    int input = Convert.ToInt32(Console.ReadLine());

    HandleMenuChoice(input);
}
while (continueRunning);

void ShowMenu()
{
    Console.WriteLine();
    Console.WriteLine("Student Management System");
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. View All Students");
    Console.WriteLine("3. Exit");
}

void HandleMenuChoice(int choice)
{
    switch (choice)
    {
        case 1:
            AddStudent();
            break;
        case 2:
            ViewAllStudents();
            break;
        case 3:
            continueRunning = false;
            break;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}

void AddStudent()
{
    Console.WriteLine();
    Console.WriteLine("Adding a new student...");

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
        Id = new Random().Next(1, 100),
        FirstName = firstName,
        LastName = lastName,
        Code = code,
        Email = email
    };

    studentService.AddStudent(newStudent);
    Console.WriteLine("\nStudent added successfully!");
}

void ViewAllStudents()
{
    Console.WriteLine("\nList of all students:");

    var students = studentService.GetAllStudents();

    foreach (var student in students)
    {
        studentService.PrintInfo(student);
    }
}