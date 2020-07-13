using System;
using System.Collections.Generic;
using API.Model;

namespace API.Features._StockManagemnt
{
    public class StockManagemntDto
    {
		public Guid Id { get; set; }
		public string Title { get; set; }
		public virtual StockCat Category { get; set; }
		public string QtyType { get; set; }
		public int RequiredStock { get; set; }
		public int CurrentStock { get; set; }
		public bool ShopTag { get; set; }
    }
}
