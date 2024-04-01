
using Newtonsoft.Json;
using OfficeOpenXml;

public class JBooks
{
    public Bookstore bookstore { get; set; }
}

public class Bookstore
{
    public List<JBook> book { get; set; }
}

public class JBook
{
    public Title title { get; set; }
    public object author { get; set; } 
    public int year { get; set; }
    public decimal price { get; set; }
    public string _category { get; set; }
    public string _cover { get; set; }
}

public class Title
{
    public string _lang { get; set; }
    public string __text { get; set; }
}


namespace Assignment_3_Data_Formats
{
	public class ReadJSONTask
	{
        public void ReadJSON(string filePath)
        {
            Console.WriteLine("\n======================================= Read JSON File ==========================================\n\n");

            Console.WriteLine("Begin");

            string jsonString = File.ReadAllText(filePath);

            //Console.WriteLine(jsonString);


            // Deserialize the JSON to the BookStore object
            JBooks books = JsonConvert.DeserializeObject<JBooks>(jsonString);

            Console.WriteLine($"{"Category",-30} {"Title",-30} {"Author",-60} {"Price",-30}");

            foreach (var book in books.bookstore.book)
            {
                string author = GetAuthorNames(book.author);

                Console.WriteLine($"{book._category,-30} {book.title.__text,-30} {author,-60} {book.price,-30}");

            }

            CreateExcelFile(books.bookstore.book, @"../../../JSON_books.xlsx");

        }

        private static string GetAuthorNames(object author)
        {


            if (author is string singleAuthor)
            {
                return singleAuthor;
            }
            else if (author is Newtonsoft.Json.Linq.JArray authorList)
            {

                return string.Join(", ", authorList);
            }
            return "Unknown Author";
        }

        private void CreateExcelFile(List<JBook> books, string filePath)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // prevent license error
            ExcelPackage excelPackage = new ExcelPackage();

            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Books");

            worksheet.Cells[1, 1].Value = "Category";
            worksheet.Cells[1, 2].Value = "Title";
            worksheet.Cells[1, 3].Value = "Author";
            worksheet.Cells[1, 4].Value = "Year";
            worksheet.Cells[1, 5].Value = "Price";

            int row = 2;
            foreach (var book in books)
            {
                string authors = GetAuthorNames(book.author);

                worksheet.Cells[row, 1].Value = book._category;
                worksheet.Cells[row, 2].Value = book.title.__text;
                worksheet.Cells[row, 3].Value = authors;
                worksheet.Cells[row, 4].Value = book.year;
                worksheet.Cells[row, 5].Value = book.price;
                row++;
            }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            FileInfo fi = new FileInfo(filePath);
            excelPackage.SaveAs(fi);
            Console.Write("\n save file SUCCESS! \n");

        }


    }
}

