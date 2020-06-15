using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class CategoryColores
    {
        public Guid Id { get; set; }
        public string configid { get; set; }
        public Guid CatlogId { get; set; }
        public string Color { get; set; }
    }
}
