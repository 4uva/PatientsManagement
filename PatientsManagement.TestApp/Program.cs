using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PatientsManagement.Common.Models;
using PatientsManagement.SDK;

namespace PatientsManagement.TestApp
{
    class Program
    {
        static readonly Uri uri = new Uri("https://localhost");
        static readonly int port = 44374;
        static readonly PatientTool tool = new PatientTool(uri, port);

        static async Task Main(string[] args)
        {
            var p1 = new Patient()
            {
                Name = "Филипп",
                Surname = "Хрюшин",
                PrimaryPhone = "+380123456789",
                AdditionalContacts = new List<AdditionalContact>()
                {
                    new AdditionalContact() { Kind = ContactKind.Home, Phone = "+380987654321" }
                }
            };
            
            var id1 = await tool.AddAsync(p1);
            Console.WriteLine($"Successfully added {id1}");
            
            var p1fromServer = await tool.GetAsync(id1);
            Console.WriteLine($"Successfully obtained back {StringifyPatient(p1fromServer)}");
            
            p1fromServer.PrimaryPhone = "+380444444444";
            await tool.ModifyAsync(id1, p1fromServer);
            Console.WriteLine($"Successfully modified {id1}");

            await PerformSearch("Филипп");  // works
            await PerformSearch("липп");    // works
            await PerformSearch("+380"); // doesn't work
            await PerformSearch("380444444444"); // doesn't work
            await PerformSearch("+380444444444"); // doesn't work
            await PerformSearch("444");           // works
            await PerformSearch("Филипп dhdhdhdh"); // works
            await PerformSearch("Филипп 444");    // works
            await PerformSearch("987654");    // works

            await tool.DeleteAsync(id1);
            Console.WriteLine($"Successfully deleted {id1}");
        }

        static async Task PerformSearch(string queryString)
        {
            var resultList = await tool.SearchAsync(queryString);
            Console.WriteLine($"Search for {queryString} produced {resultList.Count} results:");
            foreach (var p in resultList)
                Console.WriteLine(StringifyPatient(p));
        }

        static async Task CleanIndex()
        {
            var resp = await new System.Net.Http.HttpClient().DeleteAsync("http://localhost:9200/_all");
            resp.EnsureSuccessStatusCode();
            var s = await resp.Content.ReadAsStringAsync();
        }

        static string StringifyPatient(Patient p) => $"{p.Name} {p.Surname}";
    }
}
