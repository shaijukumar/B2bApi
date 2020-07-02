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

namespace API.Features._OrderMaster
{
    public class Edit
    {
        public class Command : IRequest<OrderMasterDto>
        {            
            
		public Guid Id { get; set; }
		public virtual AppUser Reseller { get; set; }
		public virtual AppUser Supplier { get; set; }
		public virtual Catalog Catalog { get; set; }
		public int Qty { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public string ShippingAddress { get; set; }
		public string BillingAddress { get; set; }
		public string Status { get; set; }
		public virtual ICollection<OrderTransactions> Transactions { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Reseller).NotEmpty();
				RuleFor(x => x.Supplier).NotEmpty();
				RuleFor(x => x.Catalog).NotEmpty();
				RuleFor(x => x.Qty).NotEmpty();
				RuleFor(x => x.Size).NotEmpty();
				RuleFor(x => x.ShippingAddress).NotEmpty();
				RuleFor(x => x.BillingAddress).NotEmpty();
				
            }

            private object RRuleFor(Func<object, object> p)
            {
                throw new NotImplementedException();
            }
        }

        public class Handler : IRequestHandler<Command, OrderMasterDto>
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

            public async Task<OrderMasterDto> Handle(Command request, CancellationToken cancellationToken)
            {
                //var test = request.test;

                var orderMaster = await _context.OrderMasters
                    .FindAsync(request.Id);
                if (orderMaster == null)
                    throw new RestException(HttpStatusCode.NotFound, new { OrderMaster = "Not found" });

				//orderMaster.Reseller  = request.Reseller ?? orderMaster.Reseller;
				//orderMaster.Supplier  = request.Supplier ?? orderMaster.Supplier;
				//orderMaster.Catalog  = request.Catalog ?? orderMaster.Catalog;
				//orderMaster.Qty  = request.Qty ?? orderMaster.Qty;
				//orderMaster.Size  = request.Size ?? orderMaster.Size;
				//orderMaster.Color  = request.Color ?? orderMaster.Color;
				//orderMaster.ShippingAddress  = request.ShippingAddress ?? orderMaster.ShippingAddress;
				//orderMaster.BillingAddress  = request.BillingAddress ?? orderMaster.BillingAddress;
				//orderMaster.Status  = request.Status ?? orderMaster.Status;
				//orderMaster.Transactions  = request.Transactions ?? orderMaster.Transactions;
				
				
				// _context.Entry(cl).State = EntityState.Modified;  //.Entry(user).State = EntityState.Added; /
				var success = await _context.SaveChangesAsync() > 0;                   
				//if (success) return Unit.Value;
				if (success)
				{
					var toReturn = _mapper.Map<OrderMaster, OrderMasterDto>(orderMaster);
					return toReturn;
				}


                throw new Exception("Problem saving changes");
            }
        }

    }
}
