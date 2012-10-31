using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFebvre.AgileZen
{
    public class UserData
    {
        public UserData(string json)
        {
            var parsed = JObject.Parse(json);
            Id = parsed["id"].Value<int>();
            Name = parsed["name"].Value<string>();
            Username = parsed["userName"].Value<string>();
            Email = parsed["email"].Value<string>();
            Created = parsed["createTime"].Value<DateTime>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }

        public string ToJson()
        {
            var parser = new JObject();
            parser.Add("id", JToken.Parse(Id.ToString()));
            parser.Add("name", JToken.Parse(Name));
            parser.Add("username", JToken.Parse(Username));
            parser.Add("email", JToken.Parse(Email));
            parser.Add("createTime", JToken.Parse(Created.ToString()));

            return parser.ToString();
        }
    }
}
