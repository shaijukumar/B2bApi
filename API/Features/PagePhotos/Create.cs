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

namespace API.Features._PagePhotos
{
    public class Create
    {
        public class Command : IRequest<PagePhotosDto>
        {

		public string Url { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
RuleFor(x => x.Url).NotEmpty();
				
            }
        }

        public class Handler : IRequestHandler<Command, PagePhotosDto>
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

            public async Task<PagePhotosDto> Handle(Command request, CancellationToken cancellationToken)
            {                                                   
                var pagePhotos = new PagePhotos
                {
					Url  = request.Url                  
                };

        _context.PagePhotoss.Add(pagePhotos);
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    var toReturn = _mapper.Map <PagePhotos, PagePhotosDto>(pagePhotos);
                    return toReturn;
                }                

                throw new Exception("Problem saving changes");
}
        }
    }
}
