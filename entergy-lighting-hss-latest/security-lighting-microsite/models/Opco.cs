using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace security_lighting_microsite.models
{
    public class Opco
    {
        public static string GetOpcoTLA(string OpcoName)
        {
            switch (OpcoName.ToLower())
            {
                case "texas":
                    return "ETI";

                case "mississippi":
                    return "EMI";

                case "louisiana":
                    return "ELL";

                case "arkansas":
                    return "EAI";

                case "new-orleans":
                    return "ENO";

                default:
                    return null;
            }
        }

        public static string GetOpcoFullName(string OpcoName)
        {
            // return the name with the first letter capitalized
            return OpcoName == "new-orleans" ? "New Orleans" : OpcoName.First().ToString().ToUpper() + OpcoName.Substring(1);
        }

        public static string GetOpcoEmailAddress(string OpCoName)
        {
            switch (OpCoName)
            {
                case "texas":
                    return ConfigurationManager.AppSettings["texasEmail"];

                case "mississippi":
                    return ConfigurationManager.AppSettings["mississippiEmail"];

                case "lousiana":
                    return ConfigurationManager.AppSettings["louisianaEmail"];

                case "arkansas":
                    return ConfigurationManager.AppSettings["arkansasEmail"];

                case "new-orleans":
                    return ConfigurationManager.AppSettings["neworleansEmail"];

                default:
                    return ConfigurationManager.AppSettings["defaultEmail"];
            }
        }
    }
}