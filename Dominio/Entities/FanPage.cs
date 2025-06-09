using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class FanPage
    {
        public string access_token { get; set; }
        public string category { get; set; }
        public List<Category> category_list { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public List<string> tasks { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
