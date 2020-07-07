using System;
using System.Threading;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Model;


namespace API.Features.PageCategory
{
    public class Create
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
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var pageItemCategory = new PageItemCategory
                {
                    Id = request.Id,
                    ParentId = request.ParentId,
                    Name = request.Name,
                    Description = request.Description              
                };

                _context.PageItemCategorys.Add(pageItemCategory);
                
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
            
    }
}
