
using HomeTask.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

public class ContactController : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Send(ContactViewModel model)
    {
        if (!ModelState.IsValid) return View("~/Views/Home/ContactMe.cshtml", model);

        try
        {
          
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("elcan4361@gmail.com", "ktrp rpsa wocg zyda"),
                EnableSsl = true,
            };

           
            var mailToMe = new MailMessage
            {
                From = new MailAddress("elcan4361@gmail.com", "My Portfolio Site"),
                Subject = $"Новое сообщение от {model.Name}",
                Body = $"Имя: {model.Name}\nEmail: {model.Email}\nСообщение: {model.Message}",
                IsBodyHtml = false,
            };
            mailToMe.To.Add("elcan4361@gmail.com");

     
            var mailToUser = new MailMessage
            {
                From = new MailAddress("elcan4361@gmail.com", "Elcan Dev"),
                Subject = "Спасибо за ваше сообщение!",
                Body = $"Здравствуйте, {model.Name}!<br><br>Спасибо, что написали мне. Я получил ваше сообщение и отвечу вам в ближайшее время.<br><br>С уважением, Эльджан.",
                IsBodyHtml = true,
            };
            mailToUser.To.Add(model.Email);

            await smtpClient.SendMailAsync(mailToMe);
            await smtpClient.SendMailAsync(mailToUser);

            ViewBag.Success = "Сообщение успешно отправлено!";
        }
        catch (Exception ex)
        {
            ViewBag.Error = "Ошибка при отправке: " + ex.Message;
        }

        return View("~/Views/Home/ContactMe.cshtml");
    }
}