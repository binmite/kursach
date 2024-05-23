using DocumentFormat.OpenXml.Office2010.Excel;
using System.Net;
using System.Text;
using System.Text.Json;
using курсач;
using курсач.Admin;
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
}
