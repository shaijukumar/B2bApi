using System;
using System.Threading;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Errors;
using API.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;

namespace API.Features._OrderMaster
{
    public class Create
    {
        public class Command : IRequest<OrderMasterDto>
        {

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
                var orderMaster = new OrderMaster
                {
					Reseller  = request.Reseller,
					Supplier  = request.Supplier,
					Catalog  = request.Catalog,
					Qty  = request.Qty,
					Size  = request.Size,
					Color  = request.Color,
					ShippingAddress  = request.ShippingAddress,
					BillingAddress  = request.BillingAddress,
					Status  = request.Status,
					Transactions  = request.Transactions                  
                };

        _context.OrderMasters.Add(orderMaster);
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    var toReturn = _mapper.Map <OrderMaster, OrderMasterDto>(orderMaster);
                    return toReturn;
                }                

                throw new Exception("Problem saving changes");
}
        }
    }
}
