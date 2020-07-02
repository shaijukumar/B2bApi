using System;
using System.Collections.Generic;

namespace API.Model
{
    public class OrderMaster
    {
		public Guid Id { get; set; }
		public virtual AppUser Reseller { get; set; }
		public virtual AppUser Supplier { get; set; }
		public virtual Catalog Catalog { get; set; }
		public int Qty { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public string ShippingAddress { get; set; }
		public string BillingAddress { get; set; }
		public string Status { get; set; }
		public virtual ICollection<OrderTransactions> Transactions { get; set; }
    }
}

















