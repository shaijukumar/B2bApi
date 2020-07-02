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

namespace API.Features._OrderTransactions
{
    public class Edit
    {
        public class Command : IRequest<OrderTransactionsDto>
        {            
            
		public Guid Id { get; set; }
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

            private object RRuleFor(Func<object, object> p)
            {
                throw new NotImplementedException();
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
                //var test = request.test;

                var orderTransactions = await _context.OrderTransactionss
                    .FindAsync(request.Id);
                if (orderTransactions == null)
                    throw new RestException(HttpStatusCode.NotFound, new { OrderTransactions = "Not found" });

				//orderTransactions.TimeStamp  = request.TimeStamp ?? orderTransactions.TimeStamp;
				//orderTransactions.Action  = request.Action ?? orderTransactions.Action;
				//orderTransactions.Comment  = request.Comment ?? orderTransactions.Comment;
				//orderTransactions.Attachments  = request.Attachments ?? orderTransactions.Attachments;
				
				
				// _context.Entry(cl).State = EntityState.Modified;  //.Entry(user).State = EntityState.Added; /
				var success = await _context.SaveChangesAsync() > 0;                   
				//if (success) return Unit.Value;
				if (success)
				{
					var toReturn = _mapper.Map<OrderTransactions, OrderTransactionsDto>(orderTransactions);
					return toReturn;
				}


                throw new Exception("Problem saving changes");
            }
        }

    }
}
