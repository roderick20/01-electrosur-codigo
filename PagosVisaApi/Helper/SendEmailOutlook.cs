using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Net.Mime;

namespace Electrosur.Helper
{
    public class SendEmailOutlook
    {
        public String ToEmail { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }

        public String WebRootPath { get; set; }

        public SendEmailOutlook(String ToEmail, String Subject, String Body, String WebRootPath)
        {
            this.ToEmail = ToEmail;
            this.Subject = Subject;
            this.Body = Body;
            this.WebRootPath = WebRootPath;
        }

        public String Error { get; set; }

        public bool Send()
        {
            try
            {

                var fromAddress = new MailAddress("pagos@electrosur.com.pe", "Electrosur");
                var toAddress = new MailAddress(ToEmail);//, "To Name");
                //const string fromPassword = "Aladino?09";
                string subject = this.Subject;


                var smtp = new SmtpClient
                {
                    Host = "191.168.5.205",
                    Port = 25,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false//,
                    //Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                String code2 = Guid.NewGuid().ToString();
                string html = this.Body.Replace("$$CODEIMAGEN$$", code2); ;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
                LinkedResource img = new LinkedResource(WebRootPath + "/images/logo.jpg", MediaTypeNames.Image.Jpeg);
                img.ContentId = code2;
                htmlView.LinkedResources.Add(img);


                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject//,
                    //Body = body
                })
                {
                    message.AlternateViews.Add(htmlView);
                    smtp.Send(message);
                }


            }
            catch (Exception ep)
            {

            }


            //try
            //{

            //    var fromAddress = new MailAddress("agile.sendmail@gmail.com", "Electrosur");
            //    var toAddress = new MailAddress(ToEmail);//, "To Name");
            //    const string fromPassword = "Aladino?09";
            //    string subject = this.Subject;


            //    var smtp = new SmtpClient
            //    {
            //        Host = "smtp.gmail.com",
            //        Port = 587,
            //        EnableSsl = true,
            //        DeliveryMethod = SmtpDeliveryMethod.Network,
            //        UseDefaultCredentials = false,
            //        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            //    };

            //    String code2 = Guid.NewGuid().ToString();
            //    string html = this.Body.Replace("$$CODEIMAGEN$$", code2); ;
            //    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
            //    LinkedResource img = new LinkedResource(WebRootPath + "/images/logo.jpg", MediaTypeNames.Image.Jpeg);
            //    img.ContentId = code2;
            //    htmlView.LinkedResources.Add(img);


            //    using (var message = new MailMessage(fromAddress, toAddress)
            //    {
            //        Subject = subject//,
            //        //Body = body
            //    })
            //    {
            //        message.AlternateViews.Add(htmlView);
            //        smtp.Send(message);
            //    }


            //}
            //catch (Exception ep)
            //{

            //}


            //try
            //{
            //    String CorreoEnvio = "@ucsm.edu.pe";
            //    String CorreoPassword = "";

            //    MailMessage mM = new MailMessage();
            //    mM.From = new MailAddress(CorreoEnvio, "Electrosur");
            //    mM.To.Add(this.ToEmail);
            //    mM.Subject = this.Subject;
            //    mM.IsBodyHtml = true;
            //    //mM.Body = this.Body;

            //    String code2 = Guid.NewGuid().ToString();

            //    string html = this.Body.Replace("$$CODEIMAGEN$$", code2); ;
            //    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
            //    LinkedResource img = new LinkedResource(WebRootPath + "/images/logo.jpg", MediaTypeNames.Image.Jpeg);
            //    img.ContentId = code2;
            //    htmlView.LinkedResources.Add(img);
            //    mM.AlternateViews.Add(htmlView);

            //    SmtpClient smtp = new SmtpClient("smtp.office365.com");
            //    smtp.Port = 587;
            //    smtp.Credentials = new NetworkCredential(CorreoEnvio, CorreoPassword);
            //    smtp.EnableSsl = true;
            //    smtp.Send(mM);

            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    Error = ex.Message;
            //}

            return false;
        }
    }
}