using ArtGallery.Data.Entities;
using ArtGallery.ViewModel.Catalog.Carts;
using ArtGallery.ViewModel.Catalog.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.Common
{
    public interface IMailHelper
    {
        public bool SendMailForWithProduct(Product productstring, string mailBody);
        public bool SendMailForgotPassword(string uname, string mailBody);
        public bool ContactUsMail(ContactModelView contact, string mailBody);
        public bool SendMailForWiner(int aucId, string mailBody);
        public bool SendMailOrder(InsertCart order, string mailBody);
    }
}
