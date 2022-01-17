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

namespace ArtGallery.Application.Common
{
    public class MailHelperImp : IMailHelper
    {
        private readonly IConfiguration config;
        string connectionString = "";
        private readonly ArtGalleryDbContext context;
        public MailHelperImp(IConfiguration config, ArtGalleryDbContext context)
        {
            
            this.config = config.GetSection("MailSettings");
            this.context = context;
            connectionString = config.GetConnectionString("ArtGalleryShop");
        }
        public void SendMailForWithProduct(Product product, string mailBody)
        {
            //getList Category In Product
            var ProdICate = context.ProductInCategories.Where(p=>p.ProductId.Equals(product.Id));
            List<int> cate = new List<int>();
            foreach (var item in ProdICate)
            {
                cate.Add(item.CategoryId);

            }

            //getList Profile liked Category
            List<ProfileUser> profiles = new List<ProfileUser>();
            var CateIProf = context.CategoryInProfiles.ToList();
            List<int> ListProfileID = new List<int>();
            foreach (var item in cate)
            {
                var c = context.CategoryInProfiles.Where(p=>p.CategoryId.Equals(item));
                foreach (var i in c)
                {
                    ListProfileID.Add(i.ProfileId);
                }
            }
            //Distinct ProfileID is duplicate
            ListProfileID = ListProfileID.Distinct().ToList();

            //Get List MailAddress from ListProfileID
            List<string> MailAddress = new List<string>();
            var ProfileUser = context.ProfileUsers.ToList();

            foreach (var item in ProfileUser)
            {
                if (item.Email != null)
                {
                    MailAddress.Add(item.Email);
                }
            }

            //send Mail
            var from = config.GetSection("MailSettings").GetSection("Mail").Value;
            var fromDisplayName = config.GetSection("MailSettings").GetSection("DisplayName").Value;
            var fromPassword = config.GetSection("MailSettings").GetSection("Password").Value;
            var host = config.GetSection("MailSettings").GetSection("Host").Value;
            var port = config.GetSection("MailSettings").GetSection("Port").Value;
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

        }

    }
}
