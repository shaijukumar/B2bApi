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

namespace API.Features._StockManagemnt
{
    public class Edit
    {
        public class Command : IRequest<StockManagemntDto>
        {            
            
		public Guid Id { get; set; }
		public string Title { get; set; }
		public virtual StockCat Category { get; set; }
		public string QtyType { get; set; }
		public int RequiredStock { get; set; }
		public int CurrentStock { get; set; }
		public bool ShopTag { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty();
				RuleFor(x => x.Category).NotEmpty();
				RuleFor(x => x.QtyType).NotEmpty();
				
            }

            private object RRuleFor(Func<object, object> p)
            {
                throw new NotImplementedException();
            }
        }

        public class Handler : IRequestHandler<Command, StockManagemntDto>
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

            public async Task<StockManagemntDto> Handle(Command request, CancellationToken cancellationToken)
            {
                //var test = request.test;

                var stockManagemnt = await _context.StockManagemnts
                    .FindAsync(request.Id);
                if (stockManagemnt == null)
                    throw new RestException(HttpStatusCode.NotFound, new { StockManagemnt = "Not found" });

				stockManagemnt.Title  = request.Title ?? stockManagemnt.Title;
				stockManagemnt.Category  = request.Category ?? stockManagemnt.Category;
				stockManagemnt.QtyType  = request.QtyType ?? stockManagemnt.QtyType;
                stockManagemnt.RequiredStock = request.RequiredStock;// ?? stockManagemnt.RequiredStock;
                stockManagemnt.CurrentStock = request.CurrentStock; // ?? stockManagemnt.CurrentStock;
                stockManagemnt.ShopTag = request.ShopTag; // ?? stockManagemnt.ShopTag;


                // _context.Entry(cl).State = EntityState.Modified;  //.Entry(user).State = EntityState.Added; /
                var success = await _context.SaveChangesAsync() > 0;                   
				//if (success) return Unit.Value;
				if (success)
				{
					var toReturn = _mapper.Map<StockManagemnt, StockManagemntDto>(stockManagemnt);
					return toReturn;
				}


                throw new Exception("Problem saving changes");
            }
        }

    }
}
