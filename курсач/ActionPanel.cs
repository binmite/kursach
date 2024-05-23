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
                        Console.WriteLine("1. Просмотреть данные клиентов записей");
                        Console.WriteLine("2. Добавить клиента запись");
                        Console.WriteLine("3. Редактировать данные клиента");
                        Console.WriteLine("4. Удалить клиента запись");

                        Console.WriteLine("\n0. Назад");

                        switch (Program.Choice(0, 5))
                        {
                            case 1:
                                Console.Clear();
                                var users = UserHelper.GetUsers();

                                foreach (var user in users)
                                {
                                    Console.WriteLine($"ID: {user.Id}\nПолное имя: {user.FullName}\nНомер телефона: {user.PhoneNumber}\n");
                                }

                                Console.WriteLine("0. Главное меню");

                                switch (Program.Choice(0, 0))
                                {
                                    case 0:
                                        MainMenu();
                                        break;
                                }
                                break;

                            case 2:
                                Console.Clear();
                                Console.Write("Введите имя нового клиента: ");
                                string name = Console.ReadLine()!;
                                Console.Write("Введите номер телефона нового клиента: ");
                                string phoneNumber = Console.ReadLine()!;

                                UserHelper.AddUser(name, phoneNumber);

                                Console.WriteLine("0. Главное меню");

                                switch (Program.Choice(0, 0))
                                {
                                    case 0:
                                        MainMenu();
                                        break;
                                }

                                break;

                            case 3:
                                Console.Clear();
                                Console.WriteLine("Список клиентов:");
                                users = UserHelper.GetUsers();
                                foreach (var user in users)
                                {
                                    Console.WriteLine($"ID: {user.Id}\nПолное имя: {user.FullName}\nНомер телефона: {user.PhoneNumber}\n");
                                }

                                Console.Write("Введите ID клиента, которого хотите изменить: ");
                                int id = int.Parse(Console.ReadLine()!);
                                UserHelper.EditUser(id);

                                Console.WriteLine("\n0. Главное меню");

                                switch (Program.Choice(0, 0))
                                {
                                    case 0:
                                        MainMenu();
                                        break;
                                }
                                break;

                            case 4:
                                Console.Clear();
                                Console.WriteLine("Список клиентов:");
                                users = UserHelper.GetUsers();
                                foreach (var user in users)
                                {
                                    Console.WriteLine($"ID: {user.Id}\nПолное имя: {user.FullName}\nНомер телефона: {user.PhoneNumber}\n");
                                }

                                Console.Write("Введите ID клиента, которого хотите удалить: ");
                                id = int.Parse(Console.ReadLine()!);
                                UserHelper.RemoveUser(id);

                                Console.WriteLine("\n0. Главное меню");

                                switch (Program.Choice(0, 0))
                                {
                                    case 0:
                                        MainMenu();
                                        break;
                                }
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
                        Console.WriteLine("\n0. Главное меню");
                        switch (Program.Choice(0, 2))
                        {
                            case 1:
                                Console.Clear();
                                var list = MedicalCardHelper.GetMedicalCards();
                                ExcelHelper<MedicalCard>.CreateExcelFile(list);
                                Console.WriteLine("Данные занесены в таблицу");
                                Console.WriteLine("\n0. Главное меню");
                                switch (Program.Choice(0, 0))
                                {
                                    case 0:
                                        MainMenu();
                                        break;
                                }
                                break;
                            case 2:
                                Console.Clear();

                                var directory = Directory.GetCurrentDirectory();
                                var projectDirectory = Directory.GetParent(directory).Parent.Parent.FullName;
                                
                                string folderPath = Path.Combine(projectDirectory, "Excel");

                                ExcelHelper<MedicalCard>.DeleteExcelFile(folderPath);
                                
                                Console.WriteLine("\n0. Главное меню");
                                switch (Program.Choice(0, 0))
                                {
                                    case 0:
                                        MainMenu();
                                        break;
                                }
                                break;
                            case 0:
                                MainMenu();
                                break;
                        }
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("1. Просмотреть данные мед. карт");
                        Console.WriteLine("2. Добавить мед. карту");
                        Console.WriteLine("3. Редактировать данные мед. карты");
                        Console.WriteLine("4. Удалить мед. карту");
                        Console.WriteLine("5. Поиск мед. карт");
                        Console.WriteLine("6. Сортировка мед. карт");

                        Console.WriteLine("\n0. Главное меню");

                        switch (Program.Choice(0, 7))
                        {
                            case 1:
                                Console.Clear();
                                Console.Clear();
                                MedicalCardHelper.ReadMedicalCards();
                                Console.WriteLine("\n0. Главное меню");
                                switch (Program.Choice(0, 0))
                                {
                                    case 0:
                                        MainMenu();
                                        break;
                                }
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("Список мед. карт:");
                                MedicalCardHelper.ReadMedicalCards();
                                Console.Write("\nВведите имя питомца: ");
                                string petName = Console.ReadLine()!;
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
                                Console.Write("\nВведите возраст питомца: ");
                                int age = int.Parse(Console.ReadLine()!);
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
                                MedicalCardHelper.AddMedicalCard(petName, kindOfPet, age, surgeries, sex);

                                Console.WriteLine("\n0. Главное меню");
                                switch (Program.Choice(0, 0))
                                {
                                    case 0:
                                        MainMenu();
                                        break;
                                }
                                break;
                            case 3:
                                Console.Clear();
                                Console.WriteLine("Список мед. карт:");
                                MedicalCardHelper.ReadMedicalCards();
                                Console.Write("Введите ID питомца, данные которого хотите изменить: ");
                                int id = int.Parse(Console.ReadLine()!);
                                MedicalCardHelper.EditMedicalCard(id);

                                Console.WriteLine("\n0. Главное меню");
                                switch (Program.Choice(0, 0))
                                {
                                    case 0:
                                        MainMenu();
                                        break;
                                }
                                break;
                            case 4:
                                Console.Clear();
                                Console.WriteLine("Список мед. карт:");
                                MedicalCardHelper.ReadMedicalCards();
                                Console.Write("Введите ID питомца, карточку которого хотите удалить: ");
                                id = int.Parse(Console.ReadLine()!);
                                MedicalCardHelper.RemoveMedicalCard(id);

                                Console.WriteLine("\n0. Главное меню");
                                switch (Program.Choice(0, 0))
                                {
                                    case 0:
                                        MainMenu();
                                        break;
                                }
                                break;
                            case 5:
                                Console.Clear();
                                Console.WriteLine("Список мед. карт:");
                                MedicalCardHelper.ReadMedicalCards();
                                Console.Write("\nПоиск карты по ID\n\nВведите ID карты: ");
                                id = int.Parse(Console.ReadLine()!);
                                Console.WriteLine(MedicalCardHelper.FindDataById(id));
                                Console.WriteLine("\n0. Главное меню");
                                switch (Program.Choice(0, 0))
                                {
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
                                        Console.Clear();
                                        var cards = MedicalCardHelper.GetMedicalCards();
                                        cards.Sort(new SortByName());
                                        foreach (var card in cards)
                                        {
                                            Console.WriteLine(card);
                                        }
                                        Console.WriteLine("\n0. Главное меню");
                                        switch (Program.Choice(0, 0))
                                        {
                                            case 0:
                                                MainMenu();
                                                break;
                                        }
                                        break;
                                    case 2:
                                        Console.Clear();
                                        cards = MedicalCardHelper.GetMedicalCards();
                                        cards.Sort(new SortByAge());
                                        foreach (var card in cards)
                                        {
                                            Console.WriteLine(card);
                                        }
                                        Console.WriteLine("\n0. Главное меню");
                                        switch (Program.Choice(0, 0))
                                        {
                                            case 0:
                                                MainMenu();
                                                break;
                                        }
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
                        
                        Console.WriteLine("\n0. Главное меню");

                        switch (Program.Choice(0, 4))
                        {
                            case 1:
                                Console.Clear();
                                AppointmentHelper.ReadAppointments();
                                Console.WriteLine("\n0. Главное меню");
                                switch (Program.Choice(0, 0))
                                {
                                    case 0:
                                        MainMenu();
                                        break;
                                }
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("Выбыерите ветеринара:");
                                Console.WriteLine("1. Доктор Браун");
                                Console.WriteLine("2. Доктор Уэйн");
                                Console.WriteLine("3. Доктор Кто");
                                Console.WriteLine("4. Доктор Врач");
                                Console.WriteLine("\n0. Главное меню");
                                string doctorName = "Не выбран";

                                switch (Program.Choice(0, 4))
                                {
                                    case 1:
                                        doctorName = "Доктор Браун";
                                        break;
                                    case 2:
                                        doctorName = "Доктор Уэйн";
                                        break;
                                    case 3:
                                        doctorName = "Доктор Кто";
                                        break;
                                    case 4:
                                        doctorName = "Доктор Врач";
                                        break;
                                    case 0:
                                        MainMenu();
                                        break;
                                }

                                Console.Clear();
                                Console.WriteLine("Выберите дату:");
                                DateOnly date = DateOnly.Parse(Console.ReadLine()!);

                                Console.WriteLine("Выберите время:");
                                TimeOnly time = TimeOnly.Parse(Console.ReadLine()!);

                                Console.WriteLine("Введите Id пользователя, которому назначено посещение:");
                                var userId = int.Parse(Console.ReadLine()!);

                                AppointmentHelper.AddAppointment(doctorName, date, time, userId);

                                Console.WriteLine("\n0. Главное меню");
                                switch (Program.Choice(0, 0))
                                {
                                    case 0:
                                        MainMenu();
                                        break;
                                }
                                break;
                            case 3:
                                Console.Clear();
                                Console.Write("Список встреч:\n");
                                AppointmentHelper.ReadAppointments();
                                Console.Write("\nВведите ID встречи, которую хотите изменить: ");
                                int id = int.Parse(Console.ReadLine()!);
                                AppointmentHelper.EditAppointment(id);

                                Console.WriteLine("\n0. Главное меню");
                                switch (Program.Choice(0, 0))
                                {
                                    case 0:
                                        MainMenu();
                                        break;
                                }
                                break;
                            case 4:
                                Console.Clear();
                                Console.WriteLine("Список встреч:");
                                AppointmentHelper.ReadAppointments();
                                Console.Write("Введите ID встречи, которую хотите удалить: ");
                                id = int.Parse(Console.ReadLine()!);
                                AppointmentHelper.RemoveAppointment(id);

                                Console.WriteLine("\n0. Главное меню");
                                switch (Program.Choice(0, 0))
                                {
                                    case 0:
                                        MainMenu();
                                        break;
                                }
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
