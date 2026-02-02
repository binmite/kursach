using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using курсач.Enities;


namespace курсач.Helpers.Admin
{
    internal class ExcelHelper<T>
    {
        public static void CreateExcelFile(List<T> list)
        {
            var directory = Directory.GetCurrentDirectory();
            var projectDirectory = Directory.GetParent(directory).Parent.Parent.FullName;

            string folderPath = Path.Combine(projectDirectory, "Excel");

            string fileName = $"{typeof(T).Name}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            string filePath = Path.Combine(folderPath, fileName);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(typeof(T).Name);

                var properties = typeof(T).GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cell(1, i + 1).Value = properties[i].Name;
                }

                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    for (int j = 0; j < properties.Length; j++)
                    {
                        var value = properties[j].GetValue(item);
                        if (value != null)
                        {
                            worksheet.Cell(i + 2, j + 1).Value = value.ToString();
                        }
                        else
                        {
                            worksheet.Cell(i + 2, j + 1).Value = string.Empty;
                        }
                    }
                }

                workbook.SaveAs(filePath);
            }
        }

        public static void DeleteExcelFile(string folderPath)
        {
            try
            {
                string[] excelFiles = Directory.GetFiles(folderPath, "*.xlsx");

                if (excelFiles.Length == 0)
                {
                    Console.WriteLine("Нет файлов для удаления\n");
                    
                    return;
                }

                Console.WriteLine("Список файлов Excel в папке:");
                for (int i = 0; i < excelFiles.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Path.GetFileName(excelFiles[i])}");
                }

                Console.Write("\nВведите номер файла, который нужно удалить: ");
                int fileNumber = int.Parse(Console.ReadLine()!);

                if (fileNumber < 1 || fileNumber > excelFiles.Length)
                {
                    Console.WriteLine("\nНекорректный номер файла.");
                    return;
                }

                string fileToDelete = excelFiles[fileNumber - 1];

                File.Delete(fileToDelete);

                Console.WriteLine($"\nФайл \"{Path.GetFileName(fileToDelete)}\" успешно удален.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при удалении файла: {ex.Message}");
            }
        }
    }
}
