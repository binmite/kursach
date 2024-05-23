using System.Text;
using System.Text.Json;
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

        public static User FindDataById(int id)
        {
            var users = GetUsers();

            foreach (var user in users)
            {
                if (user.Id == id)
                    return user;
            }

            return null;
        }

        public static User FindDataByLogin(string login)
        {
            var users = GetUsers();

            foreach (var user in users)
            {
                if (user.Login == login)
                    return user;
            }

            return null;
        }

        public static void AddUser(string name, string phoneNumber) 
        {
            var users = GetUsers();

            var newUser = new User()
            {
                Id = users.Max(x => x.Id) + 1,
                FullName = name,
                PhoneNumber = phoneNumber
            };

            users.Add(newUser);

            Console.WriteLine($"Пользователь {newUser.Login} добавлен\nID пользователя: {newUser.Id}");

            Save(users);
        }

        public static void RemoveUser(int id)
        {
            var users = GetUsers();
            var userToDelete = users.FirstOrDefault(x => x.Id == id);

            users.Remove(userToDelete);

            Console.WriteLine($"Пользователь {userToDelete.Login} удалён"); 

            Save(users);
        }

        public static void EditUser(int id)
        {
            var users = GetUsers();
            var userToEdit = users.FirstOrDefault(x => x.Id == id);

            Console.WriteLine("1. Редактировать имя");
            Console.WriteLine("2. Редактировать имя");
            Console.WriteLine("0. Выход");

            switch (Program.Choice(0, 3))
            {
                case 1:
                    Console.Write("Введите новое имя: ");
                    userToEdit.FullName = Console.ReadLine()!;
                    break;
                case 2:
                    Console.Write("Введите новый номер телефона: ");
                    userToEdit.PhoneNumber = Console.ReadLine()!;
                    break;
                case 0:
                    ActionPanel.MainMenu();
                    break;
            }

            Save(users);
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
