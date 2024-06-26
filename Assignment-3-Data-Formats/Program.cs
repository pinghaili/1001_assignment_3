﻿using System;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Assignment_3_Data_Formats;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("======================================== TASK 1 ============================================\n\n");

        JsonTask jsonTask = new JsonTask();

        Console.WriteLine("-------------------------------- Fetch and Print Users -----------------------------------\n\n");

        string jsonUrl = "https://jsonplaceholder.typicode.com/users";

        jsonTask.ReadDataFromUrl(jsonUrl, data =>
        {
            jsonTask.PrintUsers(data);

            Console.WriteLine("\n\n---------------------------------- Create Users.xlsx ------------------------------------\n\n");

            jsonTask.CreateExcelFile(data, @"../../../Users.xlsx");

            task2();

        });

        Console.Read();
    }

    static private void task2() {

        Console.WriteLine("\n======================================== TASK 2 ============================================\n\n");

        ReadXMLTask readXMLTask = new ReadXMLTask();

        readXMLTask.ReadXML(@"../../../books.xml");


        ReadCSVTask readCSVTask = new ReadCSVTask();

        readCSVTask.ReadCSV(@"../../../books.csv");


        ReadJSONTask readJSONTask = new ReadJSONTask();

        readJSONTask.ReadJSON(@"../../../books.json");

    }
}