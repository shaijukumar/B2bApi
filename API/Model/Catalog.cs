using System;
using System.Collections.Generic;

namespace API.Model
{
    public class Catalog
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<CatalogPhoto> Photos { get; set; }
        public virtual AppUser Supplier { get; set; }
        public virtual ICollection<CategoryColores> Colores { get; set; }        
        public virtual ICollection<CategorySize> Sizes { get; set; }

    }
}  