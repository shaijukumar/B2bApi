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

namespace API.Features._PagePhotos
{
    public class Edit
    {
        public class Command : IRequest<PagePhotosDto>
        {            
            
		public Guid Id { get; set; }
		public string Url { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Url).NotEmpty();
				
            }

            private object RRuleFor(Func<object, object> p)
            {
                throw new NotImplementedException();
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
                //var test = request.test;

                var pagePhotos = await _context.PagePhotoss
                    .FindAsync(request.Id);
                if (pagePhotos == null)
                    throw new RestException(HttpStatusCode.NotFound, new { PagePhotos = "Not found" });

				pagePhotos.Url  = request.Url ?? pagePhotos.Url;
				
				
				// _context.Entry(cl).State = EntityState.Modified;  //.Entry(user).State = EntityState.Added; /
				var success = await _context.SaveChangesAsync() > 0;                   
				//if (success) return Unit.Value;
				if (success)
				{
					var toReturn = _mapper.Map<PagePhotos, PagePhotosDto>(pagePhotos);
					return toReturn;
				}


                throw new Exception("Problem saving changes");
            }
        }

    }
}
