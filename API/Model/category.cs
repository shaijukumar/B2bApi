using System;

namespace API.Model
{
    public class Category
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Title { get; set; }
        public string icon { get; set; }
        public string SizeType { get; set; }
    }
}