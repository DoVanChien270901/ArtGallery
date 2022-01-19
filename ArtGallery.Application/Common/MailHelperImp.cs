using ArtGallery.Data.EF;
using ArtGallery.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ArtGallery.ViewModel.Catalog.Email;

namespace ArtGallery.Application.Common
{
    public class MailHelperImp : IMailHelper
    {
        private readonly IConfiguration config;
        private readonly IHostingEnvironment hosting;
        private readonly ArtGalleryDbContext context;
        string from = "";
        string fromDisplayName = "";
        string fromPassword = "";
        string host = "";
        int port = 0;
        public MailHelperImp(IConfiguration config, ArtGalleryDbContext context, IHostingEnvironment hosting)
        {
            this.hosting = hosting;
            this.config = config.GetSection("MailSettings");
            this.context = context;
            from = "myartgalleryprojectmail@gmail.com";
            fromDisplayName = "OAG";
            fromPassword = "Art1040@";
            host = "smtp.gmail.com";
            port = 587;
        }

        public bool ContactUsMail(ContactModelView contact, string mailBody)
        {
            mailBody = mailBody.Replace("{from}", contact.FromMail);
            mailBody = mailBody.Replace("{mess}", contact.Message);

            MimeMessage email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            string mailSubject = "You have new Contact";
            email.Subject = mailSubject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailBody;
            email.Body = builder.ToMessageBody();

            email.To.Add(MailboxAddress.Parse(from));

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect(host, Convert.ToInt32(port), MailKit.Security.SecureSocketOptions.StartTls);
            smtpClient.Authenticate(from, fromPassword);
            smtpClient.Send(email);

            smtpClient.Disconnect(true);
            return true;
        }

        public bool SendMailForgotPassword(string uname, string mailBody)
        {
            var account = context.Accounts.SingleOrDefault(a=>a.Name.Equals(uname));
            if (account == null)
            {
                return false;
            }

            var profile = context.ProfileUsers.SingleOrDefault(p=>p.AccountId.Equals(account.Name));

            //send Mail
            mailBody = mailBody.Replace("{name}", account.Name);
            mailBody = mailBody.Replace("{password}", account.Password);
            string mailSubject = "Forgot Password";
            MimeMessage email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.Subject = mailSubject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailBody;
            email.Body = builder.ToMessageBody();

            email.To.Add(MailboxAddress.Parse(profile.Email));

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect(host, Convert.ToInt32(port), MailKit.Security.SecureSocketOptions.StartTls);
            smtpClient.Authenticate(from, fromPassword);
            smtpClient.Send(email);

            smtpClient.Disconnect(true);
            return true;
        }

        public bool SendMailForWithProduct(Product product, string mailBody)
        {
            //get product
            var prod = context.Products.SingleOrDefault(c => c.Id.Equals(product.Id));

            mailBody = mailBody.Replace("{title}", prod.Title);
            mailBody = mailBody.Replace("{price}", prod.Price.ToString());
            mailBody = mailBody.Replace("{date}", prod.CreateDate.ToString());
            mailBody = mailBody.Replace("{id}", prod.Id.ToString());
            string domain = "http://localhost:30162/Home/Detail/" + prod.Id;
            mailBody = mailBody.Replace("{link}", domain.ToString());

            //getList Category In Product
            var ProdICate = context.ProductInCategories.Where(p=>p.ProductId.Equals(product.Id));
            List<int> cate = new List<int>();
            foreach (var item in ProdICate)
            {
                cate.Add(item.CategoryId);

            }

            //getList ProfileID liked Category
            List<int> profileID = new List<int>();
            var CateIProf = context.CategoryInProfiles.ToList();
            foreach (var item in CateIProf)
            {
                foreach (var i in ProdICate)
                {
                    if (item.CategoryId == i.CategoryId)
                    {
                        profileID.Add(item.ProfileId);
                    }
                }
            }

            if (profileID == null)
            {
                return false;
            }
            //Distinct ProfileID is duplicate
            profileID = profileID.Distinct().ToList();

            //Get List MailAddress from ListProfileID
            List<string> MailAddress = new List<string>();
            var ProfileUser = context.ProfileUsers.ToList();

            foreach (var item in ProfileUser)
            {
                foreach (var i in profileID)
                {
                    if (item.Email != null && i == item.Id)
                    {
                        MailAddress.Add(item.Email);
                    }
                }
            }
            if (MailAddress == null)
            {
                return false;
            }

            //send Mail

            string mailSubject = "You have new favorite Art selling !!!";
            MimeMessage email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.Subject = mailSubject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailBody;
            email.Body = builder.ToMessageBody();
            foreach (var address in MailAddress)
            {
                email.To.Add(MailboxAddress.Parse(address));
            }

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect(host,Convert.ToInt32(port),MailKit.Security.SecureSocketOptions.StartTls);
            smtpClient.Authenticate(from, fromPassword);
            smtpClient.Send(email);

            smtpClient.Disconnect(true);
            return true;
        }


    }
}
