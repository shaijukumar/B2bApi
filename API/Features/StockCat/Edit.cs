using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Interfaces;
using API.Model;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features._StockCat
{
    public class Edit
    {
        public class Command : IRequest<StockCatDto>
        {            
            
		public Guid Id { get; set; }
		public string Title { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
				
            }

            private object RRuleFor(Func<object, object> p)
            {
                throw new NotImplementedException();
            }
        }

        public class Handler : IRequestHandler<Command, StockCatDto>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<StockCatDto> Handle(Command request, CancellationToken cancellationToken)
            {
                //var test = request.test;

                var stockCat = await _context.StockCats
                    .FindAsync(request.Id);
                if (stockCat == null)
                    throw new RestException(HttpStatusCode.NotFound, new { StockCat = "Not found" });

				stockCat.Title  = request.Title ?? stockCat.Title;
				
				
				// _context.Entry(cl).State = EntityState.Modified;  //.Entry(user).State = EntityState.Added; /
				var success = await _context.SaveChangesAsync() > 0;                   
				//if (success) return Unit.Value;
				if (success)
				{
					var toReturn = _mapper.Map<StockCat, StockCatDto>(stockCat);
					return toReturn;
				}


                throw new Exception("Problem saving changes");
            }
        }

    }
}
