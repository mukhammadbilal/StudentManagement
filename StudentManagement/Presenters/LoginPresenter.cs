using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Models;

namespace StudentManagement.Presenters;

public class LoginPresenter
{
    private readonly IAuthService _authService;
    private const int MaxAttempts = 3;

    public LoginPresenter(IAuthService authService)
    {
        this._authService = authService;
    }

    public void ShowMainMenu(StudentPresenter studentPresenter)
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to Student Management System");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");
            Console.ResetColor();

            Console.Write("\nSelect an option: ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                bool isAuthorized = RunLoginProcess();

                if (isAuthorized)
                {
                    studentPresenter.ShowStudentMenu();
                }
                else
                {
                    return;
                }
            }
            else if (option == "2")
            {
                Console.WriteLine("\nExiting application...");
                break;
            }
            else
            {
                Console.WriteLine("\nInvalid option, try again.");
                Console.ReadKey();
            }
        }
    }

    public bool RunLoginProcess()
    {
        int attemptsLeft = MaxAttempts;

        while (attemptsLeft > 0)
        {
            Console.WriteLine("\n----------Login----------");
            Console.Write("Enter username: ");
            string user = Console.ReadLine();
            Console.Write("Enter password: ");
            string pass = Console.ReadLine();

            var teacher = _authService.Authenticate(user, pass);

            if (teacher != null)
            {
                ShowSuccess(teacher);
                return true;
            }

            attemptsLeft--;
            ShowFailure(attemptsLeft);
        }

        return false;
    }

    private static void ShowSuccess(Teacher teacher)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Login successful! Welcome, {teacher.FirstName} {teacher.LastName}");
        Console.ResetColor();
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private static void ShowFailure(int remaining)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        if (remaining == 0)
        {
            Console.WriteLine("\nNo attempts left");
            Console.WriteLine("Exiting application...");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine($"Invalid credentials. {remaining} attempts left.");
        }

        Console.ResetColor();
    }
}
