using ArtGallery.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.ViewModel.System.Admin
{
    public class CategoryModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public Category Category { get; set; }
    }
}
