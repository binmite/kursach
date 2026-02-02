using DocumentFormat.OpenXml.Spreadsheet;
using курсач.Admin;
using курсач.Enities;
using курсач.Helpers.Admin;

namespace курсач
{
    internal class ActionPanel
    {
        public static void MainMenu()
        {
            Console.Clear();

            Console.WriteLine("Меню");
            Console.WriteLine("\n1. Работа с клиентами");
            Console.WriteLine("2. Работа с таблицами");
            Console.WriteLine("3. Работа с мед. картами");
            Console.WriteLine("4. Работа с записями к врачам");
        }

        public static void Start()
        {
            MainMenu();

            while (true)
            {
                switch (Program.Choice(1, 4))
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("1. Посмотреть данные клиентов");
                        Console.WriteLine("2. Добавить клиента");
                        Console.WriteLine("3. Редактировать данные клиента");
                        Console.WriteLine("4. Удалить клиента");

                        Console.WriteLine("\n0. Назад");

                        switch (Program.Choice(0, 5))
                        {
                            case 1:
                                Console.Clear();
                                UserHelper.ReadUsers();

                                Console.WriteLine("0. Главное меню");

                                Program.ReturnToMainMenu();
                                break;

                            case 2:
                                Console.Clear();
                                string name = Program.InputName();
                                Console.Write("Введите номер телефона нового клиента в формате +375*********: ");
                                string phoneNumber = Console.ReadLine()!;

                                UserHelper.AddUser(name, phoneNumber);

                                Console.WriteLine("0. Главное меню");
                                Program.ReturnToMainMenu();
                                break;
                            case 3:
                                Console.Clear();
                                UserHelper.ReadUsers();

                                int id = Program.InputId();
                                UserHelper.EditUser(id);

                                Console.WriteLine("\n0. Главное меню");
                                Program.ReturnToMainMenu();
                                break;
                            case 4:
                                Console.Clear();
                                UserHelper.ReadUsers();

                                id = Program.InputId();
                                UserHelper.RemoveUser(id);

                                Console.WriteLine("\n0. Главное меню");
                                Program.ReturnToMainMenu();
                                break;
                            case 0:
                                MainMenu();
                                break;
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("1. Занести данные мед. карт в таблицу");
                        Console.WriteLine("2. Удалить таблицу\n");
                        Console.WriteLine("\n0. Назад");
                        switch (Program.Choice(0, 2))
                        {
                            case 1:
                                Console.Clear();
                                var list = MedicalCardHelper.GetMedicalCards();
                                ExcelHelper<MedicalCard>.CreateExcelFile(list);
                                Console.WriteLine("Данные занесены в таблицу");
                                Console.WriteLine("\n0. Главное меню");
                                Program.ReturnToMainMenu();
                                break;
                            case 2:
                                Console.Clear();

                                var directory = Directory.GetCurrentDirectory();
                                var projectDirectory = Directory.GetParent(directory).Parent.Parent.FullName;
                                
                                string folderPath = Path.Combine(projectDirectory, "Excel");

                                ExcelHelper<MedicalCard>.DeleteExcelFile(folderPath);
                                
                                Console.WriteLine("\n0. Главное меню");
                                Program.ReturnToMainMenu();
                                break;
                            case 0:
                                MainMenu();
                                break;
                        }
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("1. Посмотреть данные мед. карт");
                        Console.WriteLine("2. Добавить мед. карту");
                        Console.WriteLine("3. Редактировать данные мед. карты");
                        Console.WriteLine("4. Удалить мед. карту");
                        Console.WriteLine("5. Поиск мед. карт");
                        Console.WriteLine("6. Сортировка мед. карт");

                        Console.WriteLine("\n0. Назад");

                        switch (Program.Choice(0, 6))
                        {
                            case 1:
                                Console.Clear();
                                Console.Clear();
                                MedicalCardHelper.ReadMedicalCards();
                                Console.WriteLine("\n0. Главное меню");
                                Program.ReturnToMainMenu();
                                break;
                            case 2:
                                Console.Clear();
                                MedicalCardHelper.ReadMedicalCards();
                                string petName = Program.InputName();
                                Console.Write("\nВведите вид питомца: ");
                                string kindOfPet = Console.ReadLine()!;
                                Console.Write("\nВведите пол питомца: ");
                                string sex = "Неопределён";
                                Console.Write("\n1. Мужской");
                                Console.Write("\n2. Женский\n");
                                switch (Program.Choice(1, 2))
                                {
                                    case 1:
                                        sex = "Мужской";
                                        break;
                                    case 2:
                                        sex = "Женский";
                                        break;
                                }
                                int age = Program.InputAge();
                                Console.WriteLine("Проводились ли над питомцем хирургиеские операции: ");
                                bool surgeries = false;
                                Console.WriteLine("1. Да");
                                Console.WriteLine("2. Нет");
                                switch (Program.Choice(1, 2))
                                {
                                    case 1:
                                        surgeries = true;
                                        break;
                                    case 2:
                                        surgeries = false;
                                        break;
                                }

                                Console.WriteLine("Список клиентов:");
                                UserHelper.ReadUsers();

                                Console.Write("\nВведите ID клиента к которому привязана мед. карта: ");
                                int userId = Program.InputId();

                                MedicalCardHelper.AddMedicalCard(petName, kindOfPet, age, surgeries, sex, userId);

                                Console.WriteLine("\n0. Главное меню");
                                Program.ReturnToMainMenu();
                                break;
                            case 3:
                                Console.Clear();
                                MedicalCardHelper.ReadMedicalCards();
                                int id = Program.InputId();
                                MedicalCardHelper.EditMedicalCard(id);

                                Console.WriteLine("\n0. Главное меню");
                                Program.ReturnToMainMenu();
                                break;
                            case 4:
                                Console.Clear();
                                MedicalCardHelper.ReadMedicalCards();
                                id = Program.InputId();
                                MedicalCardHelper.RemoveMedicalCard(id);

                                Console.WriteLine("\n0. Главное меню");
                                Program.ReturnToMainMenu();
                                break;
                            case 5:
                                Console.Clear();
                                MedicalCardHelper.ReadMedicalCards();
                                Console.WriteLine("1. Поиск по ID мед. карты");
                                Console.WriteLine("2. Поиск по ID клиента к которому привязана карта");
                                Console.WriteLine("3. Поиск по имени питомца");
                                Console.WriteLine("4. Поиск по возрасту питомца");
                                Console.WriteLine("\n0. Главное меню");
                                switch (Program.Choice(0, 4)) 
                                {
                                    case 1:
                                        id = Program.InputId();
                                        Console.Clear();
                                        Console.WriteLine(MedicalCardHelper.FindDataById(id));
                                        Console.WriteLine("\n0. Главное меню");
                                        Program.ReturnToMainMenu();
                                        break;
                                    case 2:
                                        id = Program.InputId();
                                        Console.Clear();
                                        Console.WriteLine(MedicalCardHelper.FindDataById(id));
                                        Console.WriteLine("\n0. Главное меню");
                                        Program.ReturnToMainMenu();
                                        break;
                                    case 3:
                                        petName = Program.InputName();
                                        Console.Clear();
                                        Console.WriteLine(MedicalCardHelper.FindDataByPetName(petName));
                                        Console.WriteLine("\n0. Главное меню");
                                        Program.ReturnToMainMenu();
                                        break;
                                    case 4:
                                        age = Program.InputAge();
                                        Console.Clear();
                                        Console.WriteLine(MedicalCardHelper.FindDataByPetAge(age));
                                        Console.WriteLine("\n0. Главное меню");
                                        Program.ReturnToMainMenu();
                                        break;
                                    case 0:
                                        MainMenu();
                                        break;
                                }
                                break;
                            case 6:
                                Console.Clear();
                                Console.WriteLine("По какому признаку проводить сортировку:");
                                Console.WriteLine("1. Имя");
                                Console.WriteLine("2. Возраст");
                                switch (Program.Choice(0, 2))
                                {
                                    case 1:
                                        var cards = MedicalCardHelper.GetMedicalCards();
                                        cards.Sort(new SortByName());
                                        foreach (var card in cards)
                                        {
                                            Console.WriteLine(card);
                                        }
                                        Console.WriteLine("\n0. Главное меню");
                                        Program.ReturnToMainMenu();
                                        break;
                                    case 2:
                                        cards = MedicalCardHelper.GetMedicalCards();
                                        cards.Sort(new SortByAge());
                                        foreach (var card in cards)
                                        {
                                            Console.WriteLine(card);
                                        }
                                        Console.WriteLine("\n0. Главное меню");
                                        Program.ReturnToMainMenu();
                                        break;
                                }
                                break;
                            case 0:
                                MainMenu();
                                break;
                        }
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("1. Посмотреть записи к врачам");
                        Console.WriteLine("2. Добавить запись к врачу");
                        Console.WriteLine("3. Редактировать запись к врачу");
                        Console.WriteLine("4. Удалить запись к врачу");
                        
                        Console.WriteLine("\n0. Назад");

                        switch (Program.Choice(0, 4))
                        {
                            case 1:
                                Console.Clear();
                                AppointmentHelper.ReadAppointments();
                                Console.WriteLine("\n0. Главное меню");
                                Program.ReturnToMainMenu();
                                break;
                            case 2:
                                Console.Clear();
                                MedicalCardHelper.ReadMedicalCards();
                                Console.WriteLine("Введите ID карты к котороой привязано посещение:");
                                var userId = Program.InputId();

                                Console.WriteLine("Выбыерите ветеринара:");
                                Console.WriteLine("1. Азаронок А. В.");
                                Console.WriteLine("2. Волков Г. Л.");
                                Console.WriteLine("3. Иванов О. М");
                                Console.WriteLine("4. Эдаси П. У.");
                                Console.WriteLine("\n0. Главное меню");
                                string doctorName = "Не выбран";

                                switch (Program.Choice(0, 4))
                                {
                                    case 1:
                                        doctorName = "Азаронок А. В.";
                                        break;
                                    case 2:
                                        doctorName = "Волков Г. Л.";
                                        break;
                                    case 3:
                                        doctorName = "Иванов О. М";
                                        break;
                                    case 4:
                                        doctorName = "Эдаси П. У.";
                                        break;
                                    case 0:
                                        MainMenu();
                                        break;
                                }

                                Console.WriteLine("Выберите дату:");
                                DateOnly date = DateOnly.Parse(Console.ReadLine()!);

                                Console.WriteLine("Выберите время:");
                                TimeOnly time = TimeOnly.Parse(Console.ReadLine()!);

                                AppointmentHelper.AddAppointment(doctorName, date, time, userId);

                                Console.WriteLine("\n0. Главное меню");
                                Program.ReturnToMainMenu();
                                break;
                            case 3:
                                Console.Clear();
                                AppointmentHelper.ReadAppointments();
                                int id = Program.InputId();
                                AppointmentHelper.EditAppointment(id);

                                Console.WriteLine("\n0. Главное меню");
                                Program.ReturnToMainMenu();
                                break;
                            case 4:
                                Console.Clear();
                                AppointmentHelper.ReadAppointments();
                                id = Program.InputId();
                                AppointmentHelper.RemoveAppointment(id);

                                Console.WriteLine("\n0. Главное меню");
                                Program.ReturnToMainMenu();
                                break;
                            case 0:
                                MainMenu();
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
