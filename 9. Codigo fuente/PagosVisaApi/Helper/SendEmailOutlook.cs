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
    /*******************************************************************************************
* PagosVisaWeb
* Este clase es para enviar email
* Programador: Rodercik Cusirramos Montesinos
* Fecha de creacion: 22/06/2020
* Fecha de modificacion: 03/08/2020      
* *****************************************************************************************/

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
                var toAddress = new MailAddress(ToEmail);
                string subject = this.Subject;


                var smtp = new SmtpClient
                {
                    Host = "191.168.5.205",
                    Port = 25,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false
                };

                String code2 = Guid.NewGuid().ToString();
                string html = this.Body.Replace("$$CODEIMAGEN$$", code2); ;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
                LinkedResource img = new LinkedResource(WebRootPath + "/images/logo.jpg", MediaTypeNames.Image.Jpeg);
                img.ContentId = code2;
                htmlView.LinkedResources.Add(img);


                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject
                })
                {
                    message.AlternateViews.Add(htmlView);
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {

            }                      

            return false;
        }
    }
}