using System;
using System.Collections.Generic;
using API.Model;

namespace API.Features._OrderAttachments
{
    public class OrderAttachmentsDto
    {
		public Guid Id { get; set; }
		public string Url { get; set; }
		public string AttachmentType { get; set; }
    }
}
