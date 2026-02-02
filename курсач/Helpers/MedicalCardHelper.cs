using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text;
using System.Text.Json;
using курсач.Enities;
using курсач.Helpers.Admin;

namespace курсач.Admin
{
    internal class MedicalCardHelper
    {
        public static List<MedicalCard> GetMedicalCards()
        {
            var directory = Directory.GetCurrentDirectory();
            var projectDirectory = Directory.GetParent(directory).Parent.Parent.FullName;
            string filePath = Path.Combine(projectDirectory, $"MedicalCards.txt");

            using (FileStream fstream = File.OpenRead(filePath))
            {
                byte[] buffer = new byte[fstream.Length];
                fstream.Read(buffer, 0, buffer.Length);
                string textFromFile = Encoding.Default.GetString(buffer);
                var medicalCards = JsonSerializer.Deserialize<List<MedicalCard>>(textFromFile);

                return medicalCards;
            }
        }

        public static MedicalCard FindDataById(int id)
        {
            var medicalCards = GetMedicalCards();

            foreach (var medicalCard in medicalCards)
            {
                if (medicalCard.MedicalCardId == id)
                {
                    return medicalCard;
                }
            }

            throw new InvalidOperationException($"Медицинская карта с ID = {id} не существует.");
        }

        public static MedicalCard FindDataByUserId(int id)
        {
            var medicalCards = GetMedicalCards();

            foreach (var medicalCard in medicalCards)
            {
                if (medicalCard.UserId == id)
                    return medicalCard;
            }

            throw new InvalidOperationException($"Пользователя с ID = {id} не существует.");
        }

        public static MedicalCard FindDataByPetName(string petName)
        {
            var medicalCards = GetMedicalCards();

            foreach (var medicalCard in medicalCards)
            {
                if (medicalCard.PetName == petName)
                    return medicalCard;
            }

            throw new InvalidOperationException($"Питомец с таким именем не найден");
        }

        public static MedicalCard FindDataByPetAge(int age)
        {
            var medicalCards = GetMedicalCards();

            foreach (var medicalCard in medicalCards)
            {
                if (medicalCard.Age == age)
                    Console.WriteLine($"{medicalCard}");
            }

            throw new InvalidOperationException($"Питомец с таким возрастом не найден");
        }

        public static void AddMedicalCard(string petName, string kindOfPet, int age, bool surgeries, string sex, int userId) 
        {
            var medicalCards = GetMedicalCards();

            var newMedicalCard = new MedicalCard()
            {
                MedicalCardId = medicalCards.Last().MedicalCardId + 1,
                Age = age,
                PetName = petName,
                KindOfPet = kindOfPet,
                Sex = sex,
                HasSurgeries = surgeries,
                UserId = userId
            };
            
            medicalCards.Add(newMedicalCard);

            Console.WriteLine($"Медицинская карта животного {petName} добавлена\nID животного: {newMedicalCard.MedicalCardId}");

            Save(medicalCards);
        }

        public static void RemoveMedicalCard(int id)
        {
            var medicalCards = GetMedicalCards();
            var medicalCardToDelete = medicalCards.FirstOrDefault(x => x.MedicalCardId == id);

            medicalCards.Remove(medicalCardToDelete);

            Console.WriteLine($"Карта с ID = {medicalCardToDelete.MedicalCardId} удалена");

            Save(medicalCards);
        }

        public static void EditMedicalCard(int id) 
        {
            var medicalCards = GetMedicalCards();
            var medicalCardToEdit = medicalCards.FirstOrDefault(x => x.MedicalCardId == id);

            Console.WriteLine("1. Редактировать имя");
            Console.WriteLine("2. Редактировать возраст");
            Console.WriteLine("3. Редактировать вид питомца");
            Console.WriteLine("4. Редактировать пол питомца");
            Console.WriteLine("5. Были ли проведены хирургические операции");
            Console.WriteLine("6. Изменить ID пользователя к которому привязана мед. карта");
            Console.WriteLine("0. Выход");

            switch (Program.Choice(0, 6))
            {
                case 1:
                    Console.Write("Измените имя: ");
                    medicalCardToEdit.PetName = Console.ReadLine()!;
                    break;
                case 2:
                    Console.Write("Измените возраст: ");
                    medicalCardToEdit.Age = Program.InputAge(); ;
                    break;
                case 3:
                    Console.Write("Измените вид питомца: ");
                    medicalCardToEdit.KindOfPet = Console.ReadLine()!;
                    break;
                case 4:
                    Console.Write("Измените пол питомца: ");
                    Console.Write("1. Мужской");
                    Console.Write("2. Женский");
                    switch (Program.Choice(0, 2))
                    {
                        case 1:
                            medicalCardToEdit.Sex = "Мужской";
                            break; 
                        case 2:
                            medicalCardToEdit.Sex = "Женский";
                            break;
                    }
                    break;
                case 5:
                    Console.Write("Проводились ли хирургические операции: ");
                    Console.WriteLine("\n1. Да");
                    Console.WriteLine("2. Нет");
                    switch (Program.Choice(1, 2))
                    {
                        case 1:
                            medicalCardToEdit.HasSurgeries = true;
                            break;
                        case 2:
                            medicalCardToEdit.HasSurgeries = false;
                            break;
                    }
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Список клиентов:");
                    var users = UserHelper.GetUsers();
                    foreach (var user in users)
                    {
                        Console.WriteLine($"ID: {user.UserId}\nПолное имя: {user.FullName}\nНомер телефона: {user.PhoneNumber}\n");
                    }

                    Console.Write("Введите другой ID: ");
                    medicalCardToEdit.UserId = int.Parse(Console.ReadLine()!);
                    break;
                case 0:
                    ActionPanel.MainMenu(); 
                    break;
            }

            Save(medicalCards);
        }

        public static void ReadMedicalCards()
        {
            var cards = MedicalCardHelper.GetMedicalCards();
            Console.WriteLine("Список карт:");

            foreach (var card in cards)
            {
                Console.WriteLine(card);
            }
        }

        public static void Save(List<MedicalCard> medicalCards)
        {
            var directory = Directory.GetCurrentDirectory();
            var projectDirectory = Directory.GetParent(directory).Parent.Parent.FullName;
            string filePath = Path.Combine(projectDirectory, $"MedicalCards.txt");

            var stringToSave = JsonSerializer.Serialize(medicalCards);

            using (FileStream fstream = new FileStream(filePath, FileMode.Create))
            {
                byte[] buffer = Encoding.Default.GetBytes(stringToSave);
                fstream.Write(buffer);
                Console.WriteLine("Данные сохранены");
            }
        }
    }
}
