using ArtGallery.Application.Common;
using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Carts;
using ArtGallery.ViewModel.Catalog.Email;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArtGallery.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailHelper mailServices;
        [Obsolete]
        private readonly IHostingEnvironment hosting;

        [Obsolete]
        public MailController(IMailHelper mailServices, IHostingEnvironment hosting)
        {
            this.mailServices = mailServices;
            this.hosting = hosting;
        }


        [HttpGet("sendMail/{id:int}")]
        public async Task<bool> SendMailForWithProduct(int id)
        {
            string body = string.Empty;
           

            Product product = new Product { Id = id };

            //mail template
            using (StreamReader reader = new StreamReader(hosting.WebRootPath + "\\mailTemplate\\newProductMail.html"))
            {
                body = reader.ReadToEnd();
            }
            
            return mailServices.SendMailForWithProduct(product, body);
        }

        [HttpGet("forgotPassword/{uname}")]
        public async Task<bool> SendMailForgotPassword(string uname)
        {
            string body = string.Empty;

            //mail template
            using (StreamReader reader = new StreamReader(hosting.WebRootPath + "\\mailTemplate\\forgotPassword.html"))
            {
                body = reader.ReadToEnd();
            }
            return mailServices.SendMailForgotPassword(uname, body);
        }

        [HttpGet("ContactUs/{mail}/{mess}")]
        public async Task<bool> ContactUsMail(string mail, string mess)
        {
            ContactModelView contact = new ContactModelView { 
                FromMail = mail,
                Message = mess
            };
            string body = string.Empty;
            //mail template
            using (StreamReader reader = new StreamReader(hosting.WebRootPath + "\\mailTemplate\\ContactMail.html"))
            {
                body = reader.ReadToEnd();
            }
            return mailServices.ContactUsMail(contact, body);
        }

        [HttpGet("AuctionWiner/{aucId:int}")]
        public async Task<bool> SendMailForWiner(int aucId)
        {
            string body = string.Empty;
            //mail template
            using (StreamReader reader = new StreamReader(hosting.WebRootPath + "\\mailTemplate\\mailForWinner.html"))
            {
                body = reader.ReadToEnd();
            }
            return mailServices.SendMailForWiner(aucId, body);
        }

        [HttpPost("SendMailOrder")]
        public async Task<bool> SendMailOrder(InsertCart order)
        {
            string body = string.Empty;
            //mail template
            using (StreamReader reader = new StreamReader(hosting.WebRootPath + "\\mailTemplate\\newOrder.html"))
            {
                body = reader.ReadToEnd();
            }
            return mailServices.SendMailOrder(order, body);
        }
    }
}
