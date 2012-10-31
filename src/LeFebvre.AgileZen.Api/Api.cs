using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LeFebvre.AgileZen
{
    public class Api
    {
        private const string content_type = "application/json";

        public static ApiConnection CreateConnection(string version, string token)
        {
            return new ApiConnection(version, token);
        }

        public async static Task<UserData> GetMe(ApiConnection connection)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, connection.Endpoints.Me);
            request.Headers.Add(connection.Endpoints.Headers.ApiKey, connection.Token);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(content_type));

            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return new UserData(content);
        }

        public async static Task<UserData> PutMe(ApiConnection connection, UserData user)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, connection.Endpoints.Me);
            request.Headers.Add(connection.Endpoints.Headers.ApiKey, connection.Token);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(content_type));
            request.Content = new StringContent(user.ToJson(), Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return new UserData(content);
        }
    }
}