using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFebvre.AgileZen
{
    public class ApiConnection
    {
        internal ApiConnection(string version, string token)
        {
            Version = version;
            Token = token;

            switch (Version)
            {
                case "v1":
                    Endpoints = new EndpointsV1();
                    break;
            }
        }

        public string Version { get; set; }
        public string Token { get; set; }
        public Endpoints Endpoints { get; private set; }
    }
}
