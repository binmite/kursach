using System.Text;
using System.Text.Json;
using курсач.Admin;
using курсач.Enities;

namespace курсач.Helpers.Admin
{
    internal class UserHelper
    {
        public static List<User> GetUsers()
        {
            var directory = Directory.GetCurrentDirectory();
            var projectDirectory = Directory.GetParent(directory).Parent.Parent.FullName;
            var path = Path.Combine(projectDirectory, "Users.txt");


            using (StreamReader reader = new StreamReader(path))
            {
                string textFromFile = reader.ReadToEnd();

                return JsonSerializer.Deserialize<List<User>>(textFromFile);
            }
        }

        public static void AddUser(string name, string phoneNumber) 
        {
            var users = GetUsers();

            var newUser = new User()
            {
                UserId = users.Max(x => x.UserId) + 1,
                FullName = name,
                PhoneNumber = phoneNumber
            };

            foreach (var user in users)
            {
                if (newUser.FullName == user.FullName)
                {
                    Console.WriteLine("Клиент с таким именем уже существует");
                    return;
                }

                if (newUser.PhoneNumber == user.PhoneNumber)
                {
                    Console.WriteLine("Клиент с таким номером телефона уже существует");
                    return;
                }
            }

            users.Add(newUser);

            Console.WriteLine($"Пользователь {newUser.FullName} добавлен\nID пользователя: {newUser.UserId}");

            Save(users);
        }

        public static void RemoveUser(int id)
        {
            var users = GetUsers();
            var userToDelete = users.FirstOrDefault(x => x.UserId == id);

            users.Remove(userToDelete);

            Console.WriteLine($"Пользователь {userToDelete.FullName} удалён"); 

            Save(users);
        }

        public static void EditUser(int id)
        {
            var users = GetUsers();
            var userToEdit = users.FirstOrDefault(x => x.UserId == id);

            Console.WriteLine("1. Редактировать имя");
            Console.WriteLine("2. Редактировать номер телефона");
            Console.WriteLine("0. Выход");

            switch (Program.Choice(0, 3))
            {
                case 1:
                    Console.Write("Введите новое имя: ");
                    userToEdit.FullName = Console.ReadLine()!;
                    break;
                case 2:
                    Console.Write("Введите новый номер телефона в формате +************: ");
                    userToEdit.PhoneNumber = Console.ReadLine()!;
                    break;
                case 0:
                    ActionPanel.MainMenu();
                    break;
            }

            Save(users);
        }

        public static void ReadUsers()
        {
            var users = UserHelper.GetUsers();
            Console.WriteLine("Список клиентов:");

            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.UserId}\nПолное имя: {user.FullName}\nНомер телефона: {user.PhoneNumber}\n");
            }
        }

        public static void Save(List<User> users)
        {
            var directory = Directory.GetCurrentDirectory();
            var projectDirectory = Directory.GetParent(directory).Parent.Parent.FullName;
            var path = Path.Combine(projectDirectory, "Users.txt");

            var stringToSave = JsonSerializer.Serialize(users);

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write(stringToSave);

                Console.WriteLine("Данные сохранены");
            }
        }
    }
}
