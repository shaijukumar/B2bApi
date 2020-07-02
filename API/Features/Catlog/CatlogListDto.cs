using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Features.Catlog
{
    public class CatlogListDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public string SupplierId { get; set; }
    }
}
