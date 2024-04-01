using System;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using Newtonsoft.Json;
using OfficeOpenXml;


public class User
{
    public int id { get; set; }
    public string name { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public Address address { get; set; }
}


public class Address
{
    public string street { get; set; }
    public string city { get; set; }
    public string suite { get; set; }
}


namespace Assignment_3_Data_Formats
{
	public class JsonTask
	{
		public async void ReadDataFromUrl(string jsonUrl, Action<List<User>> callback)
		{

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(jsonUrl);

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    //Console.Write(responseBody);

                    List<User> users = JsonConvert.DeserializeObject<List<User>>(responseBody);

                    callback(users);

                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        public void PrintUsers(List<User> users) {

            Console.WriteLine($"{"Name",-30} {"Email",-30} {"Phone",-30} {"Address",-30}\n");
            Console.WriteLine("============================================================================================================================================");

            for (int i = 0; i < users.Count; i++)
            {

                Console.WriteLine($"{users[i].name,-30} {users[i].email,-30} {users[i].phone,-30} {users[i].address.suite + '-' + users[i].address.street + ',' + users[i].address.city,-30}");
            }
        }


        public void CreateExcelFile(List<User>  users, string filePath) {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // prevent license error
            ExcelPackage excelPackage = new ExcelPackage();

            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Users");

            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "Name";
            worksheet.Cells[1, 3].Value = "Email";
            worksheet.Cells[1, 4].Value = "Phone";
            worksheet.Cells[1, 5].Value = "Address";

            int row = 2;
            foreach (var user in users)
            {
                worksheet.Cells[row, 1].Value = user.id;
                worksheet.Cells[row, 2].Value = user.name;
                worksheet.Cells[row, 3].Value = user.email;
                worksheet.Cells[row, 4].Value = user.phone;
                worksheet.Cells[row, 5].Value = user.address.suite + '-' + user.address.street + ',' + user.address.city;
                row++;
            }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            FileInfo fi = new FileInfo(filePath);
            excelPackage.SaveAs(fi);
            Console.Write("save file SUCCESS!");

        }
    }
}

