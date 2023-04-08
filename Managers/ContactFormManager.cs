namespace webApi.Managers
{
    public class ContactFormManager
    {
        public string ToEmail {get; set;}
        public string SMPTserver {get; set;}
        public string FromMail {get; set;}
        public string FromMailPassword {get; set;}

        public ContactFormManager(){
            ToEmail = Environment.GetEnvironmentVariable("ToEmail");
            SMPTserver = Environment.GetEnvironmentVariable("SMTPserver");
            FromMail = Environment.GetEnvironmentVariable("FromMail");
            FromMailPassword = Environment.GetEnvironmentVariable("FromMailPassword");
        }
    }
}
