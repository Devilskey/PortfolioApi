using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Text;
using System.Text.Json.Nodes;
using webApi.Managers;
using webApi.Object;
using webApi.Types;

namespace webApi.Controllers;

[ApiController]
[Route("SendContact")]
public class ContactForm : ControllerBase
{
    private string Webhook = Environment.GetEnvironmentVariable("DiscordWebhook");

    [HttpPost]
    public async Task<ActionResult> SendContactmessageAsync(ContactMail contactData)
    {
        using (HttpClient client = new HttpClient())
        {
            DiscordWebhookObject webhookObject = new DiscordWebhookObject()
            {
                username = contactData.Name,
                content = $"Subject: {contactData.Subject}   Body: {contactData.Body}"
            };

            string webhookjson = JsonConvert.SerializeObject(webhookObject);

            Console.WriteLine(webhookjson);

            var content = new StringContent(webhookjson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(Webhook, content); 

            Console.WriteLine(response.StatusCode);
        }

        return Ok();
    }
}
