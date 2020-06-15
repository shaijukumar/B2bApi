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

namespace API.Features.Catlog
{
    public class Edit
    {
        public class Command : IRequest<CatlogDto>
        {            
            public Guid Id { get; set; }
            public Guid CategoryId { get; set; }
            public string DisplayName { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public string[] Colores { get; set; }
            public CategorySize[] Sizes { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.CategoryId).NotEmpty();
            }

            private object RRuleFor(Func<object, object> p)
            {
                throw new NotImplementedException();
            }
        }

        public class Handler : IRequestHandler<Command, CatlogDto>
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

            public async Task<CatlogDto> Handle(Command request, CancellationToken cancellationToken)
            {
                //var test = request.test;

                var catalog = await _context.Catalogs
                    .FindAsync(request.Id);
                if (catalog == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Catalog = "Not found" });

                var CurrentUsername = _userAccessor.GetCurrentUsername();

                float price = 0;
                if (!float.TryParse(request.Price, out price))
                {
                    throw new Exception("invalid Price");
                }

                if (CurrentUsername.ToLower().ToString() == "admin" || catalog.Supplier.UserName == CurrentUsername)
                {                   
                    var category = await _context.Categories.SingleOrDefaultAsync(x =>
                    x.Id == request.CategoryId);

                    #region CategoryColores

                    //delete all existing entries                                        
                    _context.CategoryColores.RemoveRange(catalog.Colores);

                    //Add existing entries
                    if(request.Colores != null)
                    {
                        foreach (string c in request.Colores)
                        {
                            var color = new CategoryColores { configid = c};
                            catalog.Colores.Add(color);
                        }
                    }

                    #endregion CategoryColores

                    #region Sizes

                    //delete all existing entries
                    
                    _context.CategorySize.RemoveRange(catalog.Sizes);

                    // Add existing entries
                    if (request.Sizes != null)
                    {
                        foreach (CategorySize s in request.Sizes)
                        {
                            var size = new CategorySize { configid = s.configid, Title = s.Title, Qty = s.Qty };
                            catalog.Sizes.Add(size);
                        }
                    }
                     

                    #endregion Sizes


                    if (category != null)
                    {
                        catalog.Category = category;
                    }
                    catalog.DisplayName = request.DisplayName ?? catalog.DisplayName;
                    catalog.Description = request.Description ?? catalog.Description;
                    catalog.Price = price;


                    // _context.Entry(cl).State = EntityState.Modified;  //.Entry(user).State = EntityState.Added; /
                    var success = await _context.SaveChangesAsync() > 0;                   
                    //if (success) return Unit.Value;
                    if (success)
                    {
                        var toReturn = _mapper.Map<Catalog, CatlogDto>(catalog);
                        return toReturn;
                    }
                }
                else
                {
                    throw new Exception("Unauthorized access");
                }


                throw new Exception("Problem saving changes");
            }
        }

    }
}