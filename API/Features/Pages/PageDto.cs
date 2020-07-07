using System;

namespace Application.Pages
{
    public class PageDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
        public string URLTitle { get; set; }
        public string PageHtml { get; set; }   
        public string CategoryId { get; set; }  
        public string Category { get; set; }
        public string CategoryPath { get; set; }        
    }
}