using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Data.Entities
{
    public class CategoryInProfile
    {
        public int ProfileId { get; set; }
        public int CategoryId { get; set; }
        public ProfileUser ProfileUser { get; set; }
        public Category Category { get; set; }
    }
}
