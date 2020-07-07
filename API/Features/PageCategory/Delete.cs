using System;
using System.Threading;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using System.Net;

namespace Application.PageItemCategorys
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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

                 if (pageItemCategory.Children.Count > 0 )
                    throw new RestException(HttpStatusCode.NotFound, new { pageItemCategory = 
                        "Item is having one or more children. First remove children" });
                
                 _context.Remove(pageItemCategory);  

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
            
    }
}
