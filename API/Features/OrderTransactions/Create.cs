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

namespace API.Features._OrderTransactions
{
    public class Create
    {
        public class Command : IRequest<OrderTransactionsDto>
        {

		public DateTime TimeStamp { get; set; }
		public string Action { get; set; }
		public string Comment { get; set; }
		public virtual ICollection<OrderAttachments> Attachments { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
RuleFor(x => x.TimeStamp).NotEmpty();
				RuleFor(x => x.Action).NotEmpty();
				RuleFor(x => x.Comment).NotEmpty();
				
            }
        }

        public class Handler : IRequestHandler<Command, OrderTransactionsDto>
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

            public async Task<OrderTransactionsDto> Handle(Command request, CancellationToken cancellationToken)
            {                                                   
                var orderTransactions = new OrderTransactions
                {
					TimeStamp  = request.TimeStamp,
					Action  = request.Action,
					Comment  = request.Comment,
					Attachments  = request.Attachments                  
                };

        _context.OrderTransactionss.Add(orderTransactions);
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    var toReturn = _mapper.Map <OrderTransactions, OrderTransactionsDto>(orderTransactions);
                    return toReturn;
                }                

                throw new Exception("Problem saving changes");
}
        }
    }
}
