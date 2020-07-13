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

namespace API.Features._StockCat
{
    public class Create
    {
        public class Command : IRequest<StockCatDto>
        {

		public string Title { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
RuleFor(x => x.Title).NotEmpty();
				
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
                var stockCat = new StockCat
                {
					Title  = request.Title                  
                };

        _context.StockCats.Add(stockCat);
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    var toReturn = _mapper.Map <StockCat, StockCatDto>(stockCat);
                    return toReturn;
                }                

                throw new Exception("Problem saving changes");
}
        }
    }
}
