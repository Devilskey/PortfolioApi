using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using webApi.Managers;
using webApi.Types;

namespace webApi.Controllers;

[ApiController]
[Route("Site/[controller]")]
public class ContactForm : ControllerBase
{
    [HttpPost]
    public ActionResult<ContactForm> SendContactmessage(ContactMail contactData)
    {
        ContactFormObject contactForm = new ContactFormObject();

        MailMessage mail = new MailMessage();
        mail.To.Add(contactForm.ToEmail);
        mail.From = new MailAddress(contactForm.FromMail);
        mail.Subject = contactData.Subject;

        mail.Body = $" From {contactData.Name},  {contactData.Body}";

        mail.IsBodyHtml = true;
        using (SmtpClient smtp = new SmtpClient())
        {
            smtp.Host = contactForm.SMPTserver; //Or Your SMTP Server Address
            smtp.Credentials = new System.Net.NetworkCredential
                 (contactForm.FromMail, contactForm.FromMailPassword);
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
