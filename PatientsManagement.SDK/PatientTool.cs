using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
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

        public async Task<IReadOnlyCollection<Patient>> SearchAsync(string queryString)
        {
            var query = new Dictionary<string, string>()
            {
                ["queryString"] = queryString
            };
            var uri = new Uri(QueryHelpers.AddQueryString("Patients/search", query), UriKind.Relative);
            var resultList = await helper.GetAsync<List<Patient>>(uri);
            return resultList.AsReadOnly();
        }

        Uri GetUri() => new Uri("Patients", UriKind.Relative);
        Uri GetUriForId(int id) => new Uri($"Patients/{id}", UriKind.Relative);

        RestHelper helper;
    }
}
