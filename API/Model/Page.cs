﻿using System;

namespace API.Model
{
    public class Page
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public string CategoryId { get; set; }
        public string URLTitle { get; set; }
        public string PageHtml { get; set; }

        public virtual PageItemCategory Category { get; set; }
    }
}
