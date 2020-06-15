using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class CategorySize
    {
        public Guid Id { get; set; }
        public string configid { get; set; }
        public Guid CatlogId { get; set; }
        public string Title { get; set; }                
        public int Qty { get; set; }
        //public string color { get; set; }
    }
} 
