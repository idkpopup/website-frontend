using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Site.ServiceModels
{
    public class Contact
    {
        public String Id { get; set; }
        public String FirsName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Website { get; set; }
        public String Company { get; set; }

        public String Timezone { get; set; }

        public String MailingList { get; set; }

        public String Date { 
            get {
                return DateTime.Now.ToString("YYYY/MM/DD");
            } 
            set {}
        }
    }

    public class ContactHttpResponse
    {
        public Contact Contact { get; set; }
        public HttpStatusCode Status { get; set; }
        public DateTime Date { get; set; }
        public string ContnetType
        {
            get
            {
                return "application/json";
            }
        }
        public string Headers { get; set;  } 


    }
}
