using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using FluentValidation;
using MediatR;
 
namespace Application.Pages
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string CategoryId { get; set; }
            public string URLTitle { get; set; }
            public string PageHtml { get; set; }
        }

         public class CommandValidator : AbstractValidator<Command>{
           public CommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.CategoryId).NotEmpty();
                RuleFor(x => x.URLTitle).NotEmpty();
                RuleFor(x => x.PageHtml).NotEmpty();
            }
        }
    
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {                                
                var page  = await _context.Pages                    
                    .FindAsync(request.Id);
                if (page == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Page = "Not found" });
               
                var pageItemCategory  = await _context.PageItemCategorys                    
                    .FindAsync(Guid.Parse(request.CategoryId));
                 if (pageItemCategory == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Category = "Not found, selected Category not found" });

                page.Title = request.Title ?? page.Title;
                page.Description = request.Description ?? page.Description;
                page.Category = pageItemCategory;
                page.URLTitle = request.URLTitle ?? page.Title;
                page.PageHtml = request.PageHtml ?? page.Title;
   
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                
                throw new Exception("Problem saving changes");
            }
        }
            
    }
}