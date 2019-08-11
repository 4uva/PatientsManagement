using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PatientsManagement.SDK
{
    class RestHelper
    {
        public RestHelper(Uri baseAddress)
        {
            client = new HttpClient() { BaseAddress = baseAddress };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(Uri webApiUri)
        {
            HttpResponseMessage response = await client.GetAsync(webApiUri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<TReceive> PostAsync<TSend, TReceive>(Uri webApiUri, TSend obj)
        {
            HttpResponseMessage sendResponse = await client.PostAsJsonAsync(webApiUri, obj);
#if DEBUG
            if (!sendResponse.IsSuccessStatusCode)
            {
                string content = await sendResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Error: " + content);
            }
#endif
            sendResponse.EnsureSuccessStatusCode();
            return await sendResponse.Content.ReadAsAsync<TReceive>();
        }

        public async Task PutAsync<T>(Uri webApiUri, T obj)
        {
            HttpResponseMessage sendResponse = await client.PutAsJsonAsync(webApiUri, obj);
#if DEBUG
            if (!sendResponse.IsSuccessStatusCode)
            {
                string content = await sendResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Error: " + content);
            }
#endif
            sendResponse.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Uri webApiUri)
        {
            HttpResponseMessage sendResponse = await client.DeleteAsync(webApiUri);
#if DEBUG
            if (!sendResponse.IsSuccessStatusCode)
            {
                string content = await sendResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Error: " + content);
            }
#endif
            sendResponse.EnsureSuccessStatusCode();
        }

        readonly HttpClient client;
    }
}
