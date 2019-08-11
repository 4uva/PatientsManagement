using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using PatientsManagement.Common.Models;

namespace PatientsManagement.SDK//
{
    public class PatientTool
    {
        public PatientTool(Uri uri, int port)
        {
            var builder = new UriBuilder(uri)
            {
                Port = port,
                Path = "api/"
            };

            helper = new RestHelper(builder.Uri);
        }

        public async Task<Patient> GetAsync(int id) =>
            await helper.GetAsync<Patient>(GetUriForId(id));

        public async Task<int> AddAsync(Patient patient) =>
            await helper.PostAsync<Patient, int>(GetUri(), patient);

        public async Task ModifyAsync(int id, Patient patient) =>
            await helper.PutAsync(GetUriForId(id), patient);

        public async Task DeleteAsync(int id) =>
            await helper.DeleteAsync(GetUriForId(id));

        public async Task<List<Patient>> SearchAsync(string queryString)
        {
            var builder = new UriBuilder("Patients/search")
            {
                Query = $"queryString={queryString}"
            };
            return await helper.GetAsync<List<Patient>>(builder.Uri);
        }

        Uri GetUri() => new Uri("Patients", UriKind.Relative);
        Uri GetUriForId(int id) => new Uri($"Patients/{id}", UriKind.Relative);

        RestHelper helper;
    }
}
