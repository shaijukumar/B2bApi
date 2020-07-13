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

namespace API.Features._StockManagemnt
{
    public class Create
    {
        public class Command : IRequest<StockManagemntDto>
        {

		public string Title { get; set; }
		public Guid Category { get; set; }
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
                var category = await _context.StockCats.SingleOrDefaultAsync(x =>
                    x.Id == request.Category);

                var stockManagemnt = new StockManagemnt
                {
					Title  = request.Title,
                    Category  = category,
                    QtyType = request.QtyType,
					RequiredStock  = request.RequiredStock,
					CurrentStock  = request.CurrentStock,
					ShopTag  = request.ShopTag                  
                };

        _context.StockManagemnts.Add(stockManagemnt);
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    var toReturn = _mapper.Map <StockManagemnt, StockManagemntDto>(stockManagemnt);
                    return toReturn;
                }                

                throw new Exception("Problem saving changes");
}
        }
    }
}
