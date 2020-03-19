using System;

namespace Site.ServiceModels
{
    /// <summary>
    /// Represents a contact supplied by the registration form.
    /// </summary>
    public class Contact
    {
        public String Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Website { get; set; }
        public String Company { get; set; }
        public String Message { get; set; }

        public String Timezone { get; set; }

        public String MailingList { get; set; }

        public String Date { 
            get {
                return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            } 
            set {}
        }
    }
}
