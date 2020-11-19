using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAspCore.Models
{
    public class FormCategoryView
    {
        public string Name { get; set; }
        public string SlugCategory { get; set; }
    }
    public class FormProductView
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public int idCategory { get; set; }
    }
}
