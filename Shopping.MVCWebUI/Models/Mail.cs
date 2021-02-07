using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Shopping.MVCWebUI.Models
{
    public class Mail
    {
        public string exception;
        public string body;
        public Mail()
        {
            // göndericinin mail adresi
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;
            WebMail.UserName = "webtedarikcimkibris@gmail.com";
            WebMail.Password = "tk2021tk";
            WebMail.EnableSsl = true;

        }

        private string createBody(Cart cart, MailDetails mailDetails)
        {
            body = "<h5>" + "<u style='color:blue;'>" + "Kullanıcı Adı : " + "</u>" + mailDetails.UserName + "</h5>" +
                   "<h5>" + "<u style='color:blue;'>" + "Adı Soyadı :  " + "</u>" + mailDetails.FullName + "</h5>"+
                   "<h5>" + "<u style='color:blue;'>" + "Adres :  " + "</u>" + mailDetails.Address + "</h5>"+
                   "<h5>" + "<u style='color:blue;'>" + "Şehir :  " + "</u>" + mailDetails.City + "</h5>"+
                   "<h5>" + "<u style='color:blue;'>" + "E-Mail :  " + "</u>" + mailDetails.District + "</h5>"+
                   "<h5>" + "<u style='color:blue;'>" + "Ödeme Şekli :  " + "</u>" + mailDetails.Neighborhood + "</h5>"+
                   "<h5>" + "<u style='color:blue;'>" + "Telefon :  " + "</u>" + mailDetails.ZipCode + "</h5>";

            foreach (var cLine in cart.CartLines)
            {
                body += "<h5>" + "<u style='color:red;margin-right:1rem;'>" + "ID :    " + cLine.Product.Id + "</u>" + " Ürün İsmi :    " + cLine.Product.Name + "<u style='color:red;margin-left:1rem;'>" + " Adet : " + cLine.Quantity + "</u>";
            }

            return body;
        }

        public void sendMail(Cart cart, MailDetails mailDetails)
        {
            try
            {
                createBody(cart, mailDetails);

                // siparişlerin geleceği mail adresi
                WebMail.Send(
                      to: "siparistedarikcimkibris@gmail.com",
                      subject: "Müşteri sipariş maili",
                      body: body,
                      replyTo: "siparistedarikcimkibris@gmail.com",
                      isBodyHtml: true
                    );
            }
            catch (Exception ex)
            {

                exception = ex.Message.ToString();
            }
        }

    }
}