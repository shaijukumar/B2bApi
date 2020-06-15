using System;
using System.Collections.Generic;
using API.Model;

namespace API.Features.Catlog
{
    public class CatlogDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string SupplierName { get; set; }
        public string SupplierId { get; set; }

        public string Category { get; set; }
        public string CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<CatalogPhoto> Photos { get; set; }
        public ICollection<CategoryColores> Colores { get; set; }
        public ICollection<CategorySize> Sizes { get; set; }
    }
}