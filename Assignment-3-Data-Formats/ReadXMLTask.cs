using System;
using System.Xml;
using System.Xml.Serialization;
using OfficeOpenXml;
using static System.Reflection.Metadata.BlobBuilder;

[XmlRoot(ElementName = "books")]
public class Books
{
    [XmlElement(ElementName = "book")]
    public List<Book> Book { get; set; }
}

public class Book
{
    [XmlAttribute(AttributeName = "category")]
    public string Category { get; set; }

    [XmlElement(ElementName = "title")]
    public string Title { get; set; }

    [XmlElement(ElementName = "author")]
    public List<string> Authors { get; set; }

    [XmlElement(ElementName = "year")]
    public int Year { get; set; }

    [XmlElement(ElementName = "price")]
    public decimal Price { get; set; }
}


namespace Assignment_3_Data_Formats
{
	public class ReadXMLTask
    {
		public void ReadXML(string filePath)
		{
            Console.WriteLine("======================================= Read XML File ==========================================\n\n");

            XmlSerializer serializer = new XmlSerializer(typeof(Books));

            // XML Deserialization
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open)) {

                Books deserializedBooks = (Books)serializer.Deserialize(fileStream);

                Console.WriteLine($"{"Category",-30} {"Title",-30} {"Author",-60} {"Price",-30}");

                foreach (var book in deserializedBooks.Book)
                {
                    string authors = string.Join(", ", book.Authors);
                    Console.WriteLine($"{book.Category,-30} {book.Title,-30} {authors,-60} {book.Price,-30}");
                }

                CreateExcelFile(deserializedBooks, @"../../../XML_books.xlsx");
            }

        }


        private void CreateExcelFile(Books books, string filePath)
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
            foreach (var book in books.Book)
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

