using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using курсач;
using курсач.Enities;
using курсач.Helpers.Admin;

class Program
{
    static void Main(string[] args)
    {
        ActionPanel.Start();
    }

    public static int Choice(int min, int max)
    {
        int choice;
        
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
        {
            Console.WriteLine($"Пожалуйста, введите число от {min} до {max}:");
        }

        return choice;
    }

    public static void ReturnToMainMenu() 
    {
        switch (Program.Choice(0, 0))
        {
            case 0:
                ActionPanel.MainMenu();
                break;
        }
    }

    public static int InputId()
    {
        int id;
        Console.Write("Введите ID: ");

        while (!int.TryParse(Console.ReadLine(), out id) || id < 1)
        {
            Console.WriteLine($"Пожалуйста, введите корректный ID:");
        }

        return id;
    }

    public static int InputAge()
    {
        int age;
        Console.Write("Введите возраст: ");

        while (!int.TryParse(Console.ReadLine(), out age) || age < 1)
        {
            Console.WriteLine($"Пожалуйста, введите корректный возраст:");
        }

        return age;
    }

    public static string InputName()
    {
        Console.Write("Введите имя: ");
        string name = Console.ReadLine()!;

        while (name == "" || name == " ")
        {
            Console.WriteLine($"Пожалуйста, введите корректное имя:");
        }

        return name;
    }
}
