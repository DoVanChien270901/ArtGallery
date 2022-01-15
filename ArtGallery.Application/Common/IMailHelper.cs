﻿using ArtGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Application.Common
{
    public interface IMailHelper
    {
        public void SendMailForWithProduct(Product productstring, string mailBody);
    }
}