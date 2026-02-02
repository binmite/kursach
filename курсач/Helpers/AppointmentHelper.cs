using System.Text;
using System.Text.Json;
using курсач.Admin;
using курсач.Enities;

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
            var appointments = GetAppointments();
            Console.WriteLine("Список встреч:");

            foreach (var appointment in appointments)
            {
                Console.WriteLine(appointment);
            }
        }

        public static void AddAppointment(string doctorName, DateOnly date, TimeOnly time, int medicalCardId)
        {
            var appointments = GetAppointments();

            var newAppointment = new Appointment()
            {
                AppointmentId = appointments.Last().AppointmentId + 1,
                DoctorName = doctorName,
                Date = date,
                Time = time,
                MedicalCardId = medicalCardId
            };

            appointments.Add(newAppointment);

            Console.WriteLine($"Запись на приём к врачу {doctorName} добавлена\nID встречи: {newAppointment.AppointmentId}\nID карты питомца к которому привязана встреча: {newAppointment.MedicalCardId}");

            Save(appointments);
        }

        public static void EditAppointment(int id)
        {
            var appointments = GetAppointments();
            var appointmentToEdit = appointments.FirstOrDefault(x => x.AppointmentId == id);

            Console.WriteLine("1. Выбрать другого врача");
            Console.WriteLine("2. Изменить дату");
            Console.WriteLine("3. Изменить время");
            Console.WriteLine("4. ID карты к которой привязана встреча");
            Console.WriteLine("0. Выход");

            switch (Program.Choice(0, 3))
            {
                case 1:
                    Console.Write("Выберите другого врача: ");
                    Console.WriteLine("Выбыерите ветеринара:");
                    Console.WriteLine("1. Азаронок А. В.");
                    Console.WriteLine("2. Волков Г. Л.");
                    Console.WriteLine("3. Иванов О. М");
                    Console.WriteLine("4. Эдаси П. У.");
                    Console.WriteLine("\n0. Главное меню");
                    appointmentToEdit.DoctorName = "Не выбран";
                    switch (Program.Choice(0, 4))
                    {
                        case 1:
                            appointmentToEdit.DoctorName = "Азаронок А. В.";
                            break;
                        case 2:
                            appointmentToEdit.DoctorName = "Волков Г. Л.";
                            break;
                        case 3:
                            appointmentToEdit.DoctorName = "Иванов О. М";
                            break;
                        case 4:
                            appointmentToEdit.DoctorName = "Эдаси П. У.";
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
                case 4:
                    Console.WriteLine("Список карт:");
                    MedicalCardHelper.ReadMedicalCards();
                    Console.Write("Введите другой ID: ");
                    appointmentToEdit.MedicalCardId = int.Parse(Console.ReadLine()!);
                    break;
                case 0:
                    ActionPanel.MainMenu();
                    break;
            }

            Save(appointments);
        }

        public static void RemoveAppointment(int id)
        {
            var appointments = GetAppointments();
            var appointmentToDelete = appointments.FirstOrDefault(x => x.AppointmentId == id);

            appointments.Remove(appointmentToDelete);

            Console.WriteLine($"Встреча с ID = {appointmentToDelete.AppointmentId} удалена");

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
