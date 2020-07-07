using System;
using System.Threading;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net;
using API.Data;
using API.Interfaces;
using API.Errors;
using API.Model;

namespace Application.Pages
{
    public class Create
    { 
        public class Command : IRequest
        {
           //public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public Guid CategoryId { get; set; }

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
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _context.Pages.Where(x => x.URLTitle == request.URLTitle).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new {Email = "URLTitle already exists"});
                
 
                PageItemCategory category = await _context.PageItemCategorys.FindAsync(request.CategoryId);

                if (category == null)
                    throw new Exception("Could not find category");

                var page = new Page
                {
                    //Id = request.Id,
                    Title = request.Title,
                    Description = request.Description,
                    //CategoryId = request.CategoryId,
                    URLTitle = request.URLTitle,
                    PageHtml = request.PageHtml,
                    Category = category
                };

                _context.Pages.Add(page);
                
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
            
    }
}