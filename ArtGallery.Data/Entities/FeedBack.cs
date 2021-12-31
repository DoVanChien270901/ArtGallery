using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Data.Entities
{
    public class FeedBack
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string AccountId { get; set; }
        public Account Account { get; set; }
    }
}
