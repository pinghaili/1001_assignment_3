using System;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Assignment_3_Data_Formats;

class Program
{
    static async Task Main(string[] args)
    {
        //Console.WriteLine("======================================== TASK 1 ============================================\n\n");

        //JsonTask jsonTask = new JsonTask();

        //Console.WriteLine("-------------------------------- Fetch and Print Users -----------------------------------\n\n");

        //string jsonUrl = "https://jsonplaceholder.typicode.com/users";

        //jsonTask.ReadDataFromUrl(jsonUrl, data =>
        //{
        //    jsonTask.PrintUsers(data);

        //    Console.WriteLine("\n\n---------------------------------- Create Users.xlsx ------------------------------------\n\n");

        //    jsonTask.CreateExcelFile(data, @"../../../Users.xlsx");

        //});


        Console.WriteLine("======================================== TASK 2 ============================================\n\n");

        ReadFileTask readFileTask = new ReadFileTask();

        Books books =  readFileTask.ReadXML(@"../../../books.xml");


        readFileTask.PrintBooks(books);

        Console.Read();
    }
}