using System;
using System.Xml;
using System.Xml.Serialization;
using OfficeOpenXml;

//public class Books
//{
//    public List<Book> BookList { get; set; }
//}

//public class Book
//{
//    public string Category { get; set; }

//    public string Title { get; set; }

//    public List<string> Authors { get; set; }

//    public int Year { get; set; }

//    public decimal Price { get; set; }
//}


namespace Assignment_3_Data_Formats
{
	public class ReadCSVTask
	{
        public void ReadCSV(string filePath)
        {
            Console.WriteLine("\n======================================= Read CSV File ==========================================\n\n");


            var books = new List<Book>();
            var lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                var columns = lines[i].Split(',');

                var book = new Book
                {
                    Category = columns[0],
                    Title = columns[1],
                    Authors = new List<string> { columns[2] }, 
                    Year = int.Parse(columns[3]),
                    Price = decimal.Parse(columns[4])
                };

                books.Add(book);

            }

            Console.WriteLine($"{"Category",-30} {"Title",-30} {"Author",-60} {"Price",-30}");
            foreach (var book in books)
            {

                string authors = string.Join(", ", book.Authors);
                Console.WriteLine($"{book.Category,-30} {book.Title,-30} {authors,-60} {book.Price,-30}");
            }

            CreateExcelFile(books, @"../../../CSV_books.xlsx");

        }

        private void CreateExcelFile(List<Book> books, string filePath)
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
                string authors = string.Join(", ", book.Authors);

                worksheet.Cells[row, 1].Value = book.Category;
                worksheet.Cells[row, 2].Value = book.Title;
                worksheet.Cells[row, 3].Value = authors;
                worksheet.Cells[row, 4].Value = book.Year;
                worksheet.Cells[row, 5].Value = book.Price;
                row++;
            }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            FileInfo fi = new FileInfo(filePath);
            excelPackage.SaveAs(fi);
            Console.Write("\n save file SUCCESS! \n");

        }


    }
}

