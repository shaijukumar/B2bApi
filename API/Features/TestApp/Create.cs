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

namespace API.Features._TestApp
{
    public class Create
    {
        public class Command : IRequest<TestAppDto>
        {

		public string Title { get; set; }
		public string Description { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
RuleFor(x => x.Title).NotEmpty();
				RuleFor(x => x.Description).NotEmpty();
				
            }
        }

        public class Handler : IRequestHandler<Command, TestAppDto>
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

            public async Task<TestAppDto> Handle(Command request, CancellationToken cancellationToken)
            {                                                   
                var testApp = new TestApp
                {
					Title  = request.Title,
					Description  = request.Description                  
                };

        _context.TestApps.Add(testApp);
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    var toReturn = _mapper.Map <TestApp, TestAppDto>(testApp);
                    return toReturn;
                }                

                throw new Exception("Problem saving changes");
}
        }
    }
}
