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
        public bool SendMailForWithProduct(Product product, string mailBody)
        {
            //get product
            var prod = context.Products.SingleOrDefault(c => c.Id.Equals(product.Id));

            mailBody = mailBody.Replace("{title}", prod.Title);
            mailBody = mailBody.Replace("{price}", prod.Price.ToString());
            mailBody = mailBody.Replace("{date}", prod.CreateDate.ToString());
            mailBody = mailBody.Replace("{id}", prod.Id.ToString());


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

            //List<ProfileUser> profiles = new List<ProfileUser>();
            //var CateIProf = context.CategoryInProfiles.ToList();
            //List<int> ListProfileID = new List<int>();
            //foreach (var item in cate)
            //{
            //    var c = context.CategoryInProfiles.Where(p=>p.CategoryId.Equals(item));
            //    foreach (var i in c)
            //    {
            //        ListProfileID.Add(i.ProfileId);
            //    }
            //}
            ////Distinct ProfileID is duplicate
            //ListProfileID = ListProfileID.Distinct().ToList();

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
