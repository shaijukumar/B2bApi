using System;
using System.Collections.Generic;
using API.Model;

namespace API.Features._OrderTransactions
{
    public class OrderTransactionsDto
    {
		public Guid Id { get; set; }
		public DateTime TimeStamp { get; set; }
		public string Action { get; set; }
		public string Comment { get; set; }
		public virtual ICollection<OrderAttachments> Attachments { get; set; }
    }
}
