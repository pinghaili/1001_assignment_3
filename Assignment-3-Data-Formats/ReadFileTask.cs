using System;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName = "books")]
public class Books
{
    [XmlElement(ElementName = "book")]
    public List<Book> BookList { get; set; }
}

public class Book
{
    [XmlAttribute(AttributeName = "category")]
    public string Category { get; set; }

    [XmlElement(ElementName = "title")]
    public string Title { get; set; }

    [XmlElement(ElementName = "author")]
    public string Author { get; set; }

    [XmlElement(ElementName = "year")]
    public int Year { get; set; }

    [XmlElement(ElementName = "price")]
    public decimal Price { get; set; }
}


namespace Assignment_3_Data_Formats
{
	public class ReadFileTask
	{
		public Books ReadXML(string filePath)
		{
            Console.WriteLine("======================================= Read XML File ==========================================\n\n");

            // XML Serialization
            XmlSerializer serializer = new XmlSerializer(typeof(Books));

            //using (FileStream fileStream = new FileStream(filePath, FileMode.Create)) {
            //    serializer.Serialize(fileStream, book);// write data to file
            //    Console.WriteLine("serialization complete.XML file created on the desktop.");

            //}

            // XML Deserialization
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open)) {

                Books deserializedBooks = (Books)serializer.Deserialize(fileStream);

                return deserializedBooks;
            }

        }

        public Books ReadCSV(string filePath)
        {
            Console.WriteLine("======================================= Read CSV File ==========================================\n\n");

            // XML Serialization
            XmlSerializer serializer = new XmlSerializer(typeof(Books));

            //using (FileStream fileStream = new FileStream(filePath, FileMode.Create)) {
            //    serializer.Serialize(fileStream, book);// write data to file
            //    Console.WriteLine("serialization complete.XML file created on the desktop.");

            //}

            // XML Deserialization
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {

                Books deserializedBooks = (Books)serializer.Deserialize(fileStream);

                return deserializedBooks;
            }

        }

        public void PrintBooks(Books books)
        {

            {

                Console.WriteLine($"{"Category",-30} {"Title",-30} {"Author",-60} {"Price",-30}");

                foreach (var book in books.BookList)
                {

                    //string authors = string.Join(", ", book.Authors);
                    Console.WriteLine($"{book.Category,-30} {book.Title,-30} {book.Author,-60} {book.Price,-30}");
                }
            }
        }
    }
}

