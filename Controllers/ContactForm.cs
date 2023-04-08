using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using webApi.Managers;
using webApi.Types;

namespace webApi.Controllers
{
    [Route("Site/[controller]")]
    [ApiController]
    public class ContactForm : ControllerBase
    {
        [HttpPost]
        public ActionResult<contactFormType> SendContactmessage(contactFormType contactData)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add("ecolampjeb@gmail.com");
            mail.From = new MailAddress("noreply@devilskey.eu");
            mail.Subject = contactData.Subject;


            mail.Body = $" From {contactData.Name},  {contactData.Body}";

            mail.IsBodyHtml = true;
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = "webreus.email"; //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential
                     ("noreply@devilskey.eu", "Test?1234");
                //Or your Smtp Email ID and Password
                try
                {
                    smtp.Send(mail);
                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return BadRequest();
                }
            }
        }
    }
}
