using System;

namespace API.Features.PageCategory
{
    public class PageItemCategoryDto
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }public string Description { get; set; }

    }
}