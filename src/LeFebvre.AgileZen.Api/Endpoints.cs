using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeFebvre.AgileZen
{
    public abstract class Endpoints
    {
        public Endpoints(string version)
        {
            Root = new Uri(string.Format("https://agilezen.com/api/{0}", version));
            Me = new Uri(Root + "/me");

            switch (version)
            {
                case "v1":
                    Headers = new HeadersV1();
                    break;
            }
        }

        public Uri Root { get; private set; }
        public Uri Me { get; private set; }
        public Headers Headers { get; private set; }
    }

    public abstract class Headers
    {
        public Headers()
        {
            ApiKey = "X-Zen-ApiKey";
        }

        public string ApiKey { get; private set; }
    }

    public class EndpointsV1 : Endpoints
    {
        public EndpointsV1() : base("v1") { }
    }

    public class HeadersV1 : Headers { }
}
