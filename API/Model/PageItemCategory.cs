using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class PageItemCategory
    {

        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public virtual PageItemCategory Parent { get; set; }
        public virtual ICollection<PageItemCategory> Children { get; set; }
    }
}
