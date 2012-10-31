using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LeFebvre.AgileZen.Tests
{
    public class Me
    {
        public Me()
        {
            Connection = Api.CreateConnection(Configuration.AgileZen.Version, Configuration.ApiToken);
        }

        [Test]
        public async void Manually_get_my_user_data()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, Endpoints.Me);
            request.Headers.Add(Headers.ApiKey, Configuration.ApiToken);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(Configuration.ContentType));

            // Act
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(content.Length, Is.GreaterThan(0));
            Assert.That(json["userName"].Value<string>(), Is.EqualTo("lefebke"));
            Assert.That(json["email"].Value<string>(), Is.EqualTo("kenneth_lefebvre@keybank.com"));
        }

        private ApiConnection Connection { get; set; }

        [Test]
        public async void Request_my_user_data()
        {
            // Arrange

            // Act
            var user = await Api.GetMe(Connection);

            // Assert
            Assert.That(user.Id, Is.EqualTo(63796));
            Assert.That(user.Name, Is.EqualTo("Ken LeFebvre"));
            Assert.That(user.Username, Is.EqualTo("lefebke"));
            Assert.That(user.Email, Is.EqualTo("kenneth_lefebvre@keybank.com"));
            Assert.That(user.Created, Is.EqualTo(new DateTime(2012,10,16,22,45,57)));
        }

        [Test]
        public async void Update_my_user_data()
        {
            // Arrange
            var user = await Api.GetMe(Connection);
            var name = user.Name;

            // Act
            user.Name = name + name;
            var updated = await Api.PutMe(Connection, user);

            // Assert
            Assert.That(updated.Name, Is.EqualTo(user.Name));

            // Clean Up
            user.Name = name;
            updated = await Api.PutMe(Connection, user);
            Assert.That(updated.Name, Is.EqualTo(user.Name));
        }
    }
}
