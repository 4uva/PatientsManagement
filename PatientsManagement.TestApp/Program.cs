using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PatientsManagement.Common.Models;
using PatientsManagement.SDK;

namespace PatientsManagement.TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var uri = new Uri("https://localhost");
            var port = 44374;
            var tool = new PatientTool(uri, port);

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
            Console.WriteLine($"Successfully obtained back {p1fromServer.Name} {p1fromServer.Surname}");

            p1fromServer.PrimaryPhone = "+380444444444";
            await tool.ModifyAsync(id1, p1fromServer);
            Console.WriteLine($"Successfully modified {id1}");

            await tool.DeleteAsync(id1);
            Console.WriteLine($"Successfully deleted {id1}");
        }
    }
}
