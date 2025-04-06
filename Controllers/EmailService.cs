namespace WebApplication2.Controllers
{
    using System;
    using System.Net;
    using System.Net.Mail;

    public class EmailService
    {
        public  void SendEmail(string email,int kod)
        {
            try
            {
                MailMessage mail = new MailMessage();


                mail.From = new MailAddress(Environment.GetEnvironmentVariable("GMAIL_EMAIL")); 
                mail.To.Add(email); 

             
                mail.Subject = "Autoryzacja konta";
                mail.Body = "Witaj uzytkowniku, jeśli rejestrowałeś się do sklepu Papiezak to jest twoj kod autoryzacji "+kod;
                string HASLO = Environment.GetEnvironmentVariable("GMAIL_KEY");
               
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587); 
                smtp.EnableSsl = true; 
                smtp.Credentials = new NetworkCredential(Environment.GetEnvironmentVariable("GMAIL_EMAIL"), HASLO); 

                
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
            }
        }
    }

}
