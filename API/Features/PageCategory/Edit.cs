using System;
using System.Threading;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using System.Net;

namespace API.Features.PageCategory
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid ParentId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

         public class CommandValidator : AbstractValidator<Command>{
           public CommandValidator()
            {
                 RuleFor(x => x.Name).NotEmpty();
                 RuleFor(x => x.ParentId).NotEmpty();
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
                var pageItemCategory  = await _context.PageItemCategorys                    
                    .FindAsync(request.Id);

                if (pageItemCategory == null)
                    throw new RestException(HttpStatusCode.NotFound, new { pageItemCategory = "Not found" });

                pageItemCategory.ParentId = request.ParentId;
                pageItemCategory.Description = request.Description ?? pageItemCategory.Description;
                pageItemCategory.Name = request.Name ?? pageItemCategory.Name;;
   
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                
                throw new Exception("Problem saving changes");
            }
        }
            
    }
}
