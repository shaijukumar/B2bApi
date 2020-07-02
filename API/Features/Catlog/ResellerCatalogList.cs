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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Catlog
{
    public class ResellerCatalogList
    {
        //public class Command : IRequest<List<CatlogListDto>>
        //{
        //    public string SupplierPhone { get; set; }
        //}
        public class Query : IRequest<List<CatlogListDto>>
        {
            public string SupplierPhone { get; set; }
        }

        public class CommandValidator : AbstractValidator<Query>
        {
            public CommandValidator()
            {
                RuleFor(x => x.SupplierPhone).NotEmpty();
            }

            private object RRuleFor(Func<object, object> p)
            {
                throw new NotImplementedException();
            }
        }

        public class Handler : IRequestHandler<Query, List<CatlogListDto>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            private readonly UserManager<AppUser> _userManager;
            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper, UserManager<AppUser> userManager)
            {
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;
                _userManager = userManager;
            }           
            public async Task<List<CatlogListDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                _context.ChangeTracker.LazyLoadingEnabled = false;

                var supplier = await _context.Users
                    .FirstOrDefaultAsync(x => x.PhoneNumber == request.SupplierPhone);
               
                 
                if (supplier == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Supplier = "Not found" });
                }
               
                var catalogs = await _context.Catalogs
                    .Include(c => c.Photos)
                    .Where(x => x.Supplier == supplier)
                    .ToListAsync();

                return _mapper.Map<List<Catalog>, List<CatlogListDto>>(catalogs);


                throw new Exception("Problem saving changes");
            }
        }

    }
}