using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using курсач.Admin;
using курсач.Enities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace курсач.Helpers.Admin
{
    internal class AppointmentHelper
    {
        public static List<Appointment> GetAppointments()
        {
            var directory = Directory.GetCurrentDirectory();
            var projectDirectory = Directory.GetParent(directory).Parent.Parent.FullName;
            string filePath = Path.Combine(projectDirectory, $"Appointments.txt");

            using (FileStream fstream = File.OpenRead(filePath))
            {
                byte[] buffer = new byte[fstream.Length];
                fstream.Read(buffer, 0, buffer.Length);
                string textFromFile = Encoding.Default.GetString(buffer);
                var appointments = JsonSerializer.Deserialize<List<Appointment>>(textFromFile);

                return appointments;
            }
        }

        public static void ReadAppointments()
        {
            var appointments = AppointmentHelper.GetAppointments();

            foreach (var appointment in appointments)
            {
                Console.WriteLine(appointment);
            }
        }

        public static void AddAppointment(string doctorName, DateOnly date, TimeOnly time)
        {
            var appointments = GetAppointments();

            var newAppointment = new Appointment()
            {
                Id = appointments.Last().Id + 1,
                DoctorName = doctorName,
                Date = date,
                Time = time,
            };

            appointments.Add(newAppointment);

            Console.Clear();

            Console.WriteLine($"Запись на приём к врачу {doctorName} добавлена\nID встречи: {newAppointment.Id}");

            Save(appointments);
        }

        public static void EditAppointment(int id)
        {
            var appointments = GetAppointments();
            var appointmentToEdit = appointments.FirstOrDefault(x => x.Id == id);

            Console.WriteLine("1. Выбрать другого врача");
            Console.WriteLine("2. Изменить дату");
            Console.WriteLine("3. Изменить время");
            Console.WriteLine("0. Выход");

            switch (Program.Choice(0, 3))
            {
                case 1:
                    Console.Write("Выберите другого врача: ");
                    Console.WriteLine("Выбыерите ветеринара:");
                    Console.WriteLine("1. Доктор Браун");
                    Console.WriteLine("2. Доктор Уэйн");
                    Console.WriteLine("3. Доктор Кто");
                    Console.WriteLine("4. Доктор Врач");
                    Console.WriteLine("\n0. Главное меню");
                    appointmentToEdit.DoctorName = "Не выбран";
                    switch (Program.Choice(0, 4))
                    {
                        case 1:
                            appointmentToEdit.DoctorName = "Доктор Браун";
                            break;
                        case 2:
                            appointmentToEdit.DoctorName = "Доктор Уэйн";
                            break;
                        case 3:
                            appointmentToEdit.DoctorName = "Доктор Кто";
                            break;
                        case 4:
                            appointmentToEdit.DoctorName = "Доктор Врач";
                            break;
                        case 0:
                            ActionPanel.MainMenu();
                            break;
                    }
                    break;
                case 2:
                    Console.Write("Введите новую дату: ");
                    appointmentToEdit.Date = DateOnly.Parse(Console.ReadLine()!);
                    break;
                case 3:
                    Console.Write("Введите новое время: ");
                    appointmentToEdit.Time = TimeOnly.Parse(Console.ReadLine()!);
                    break;
                case 0:
                    ActionPanel.MainMenu();
                    break;
            }

            Save(appointments);
        }

        //public static void GetUserAppointments()
        //{
        //    var appointments = GetAppointments();

        //    foreach (var appointment in appointments)
        //    {
        //        if (Appointment.userId == user.Id)
        //            return appointment;
        //    }

        //    return null;
        //}

        public static void RemoveAppointment(int id)
        {
            var appointments = GetAppointments();
            var appointmentToDelete = appointments.FirstOrDefault(x => x.Id == id);

            appointments.Remove(appointmentToDelete);

            Console.WriteLine($"Встреча с ID = {appointmentToDelete.Id} удалена");

            Save(appointments);
        }

        public static void Save(List<Appointment> appointments)
        {
            var directory = Directory.GetCurrentDirectory();
            var projectDirectory = Directory.GetParent(directory).Parent.Parent.FullName;
            string filePath = Path.Combine(projectDirectory, $"Appointments.txt");

            var stringToSave = JsonSerializer.Serialize(appointments);

            using (FileStream fstream = new FileStream(filePath, FileMode.Create))
            {
                byte[] buffer = Encoding.Default.GetBytes(stringToSave);
                fstream.Write(buffer);
                Console.WriteLine("Данные сохранены");
            }
        }
    }
}
