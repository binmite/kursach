using DocumentFormat.OpenXml.Spreadsheet;
using System.Text;
using System.Text.Json;
using курсач.Enities;

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
                if (medicalCard.Id == id)
                    return medicalCard;
            }

            return null;
        }

        public static void AddMedicalCard(string petName, string kindOfPet, int age, bool surgeries, string sex) 
        {
            var medicalCards = GetMedicalCards();

            var newMedicalCard = new MedicalCard()
            {
                Id = medicalCards.Last().Id + 1,
                Age = age,
                PetName = petName,
                KindOfPet = kindOfPet,
                Sex = sex,
                HasSurgeries = surgeries
            };
            
            medicalCards.Add(newMedicalCard);

            Console.Clear();

            Console.WriteLine($"Медицинская карта животного {petName} добавлена\nID животного: {newMedicalCard.Id}");

            Save(medicalCards);
        }

        public static void RemoveMedicalCard(int id)
        {
            var medicalCards = GetMedicalCards();
            var medicalCardToDelete = medicalCards.FirstOrDefault(x => x.Id == id);

            medicalCards.Remove(medicalCardToDelete);

            Console.WriteLine($"Карта с ID = {medicalCardToDelete.Id} удалена");

            Save(medicalCards);
        }

        public static void EditMedicalCard(int id) 
        {
            var medicalCards = GetMedicalCards();
            var medicalCardToEdit = medicalCards.FirstOrDefault(x => x.Id == id);

            Console.WriteLine("1. Редактировать имя");
            Console.WriteLine("2. Редактировать возраст");
            Console.WriteLine("3. Редактировать вид питомца");
            Console.WriteLine("4. Редактировать пол питомца");
            Console.WriteLine("5. Были ли проведены хирургические операции");
            Console.WriteLine("0. Выход");

            switch (Program.Choice(0, 5))
            {
                case 1:
                    Console.Write("Измените имя: ");
                    medicalCardToEdit.PetName = Console.ReadLine()!;
                    break;
                case 2:
                    Console.Write("Измените возраст: ");
                    medicalCardToEdit.Age = int.Parse(Console.ReadLine()!);
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
                case 0:
                    ActionPanel.MainMenu(); 
                    break;
            }

            Save(medicalCards);
        }

        public static void ReadMedicalCards()
        {
            var cards = MedicalCardHelper.GetMedicalCards();

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
