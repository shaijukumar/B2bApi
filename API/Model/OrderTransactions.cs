using System;
using System.Collections.Generic;

namespace API.Model
{
    public class OrderTransactions
    {
		public Guid Id { get; set; }
		public DateTime TimeStamp { get; set; }
		public string Action { get; set; }
		public string Comment { get; set; }
		public virtual ICollection<OrderAttachments> Attachments { get; set; }
    }
}

















