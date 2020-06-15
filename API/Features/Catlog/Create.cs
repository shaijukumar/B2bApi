
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

namespace API.Features.Catlog
{
    public class Create
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
                RuleFor(x => x.Price).NotEmpty();
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
                var supplier = await _context.Users.SingleOrDefaultAsync(x =>
                    x.UserName == _userAccessor.GetCurrentUsername());

                var category = await _context.Categories.SingleOrDefaultAsync(x =>
                    x.Id == request.CategoryId);

                //CategoryId
                float price = 0;
                if (!float.TryParse(request.Price, out price))
                {
                    throw new Exception("invalid Price");
                }



                var cl = new List<CategoryColores>();
                //Add existing entries
                if (request.Colores != null)
                {
                    if (request.Colores != null)
                    {
                        foreach (string c in request.Colores)
                        {
                            var color = new CategoryColores
                            {
                                //Id = Guid.NewGuid(),
                                //CatlogId = request.Id,
                                configid = c
                            };
                            cl.Add(color);
                        }
                    }
                }

                var cz = new List<CategorySize>();
                if (request.Sizes != null)
                {
                    foreach (CategorySize s in request.Sizes)
                    {
                        var size = new CategorySize
                        {  configid = s.configid, Title = s.Title, Qty = s.Qty };
                        //{
                        //    Id = Guid.NewGuid(),
                        //    Title = s.Title,
                        //    Qty = s.Qty

                        //};
                        cz.Add(size);
                    }
                }

                var catalog = new Catalog
                {
                    //Id = request.Id,
                    DisplayName = request.DisplayName,
                    Description = request.Description,
                    Supplier = supplier,
                    Category = category,
                    Price = price,   
                    Colores = cl,
                    Sizes = cz
                };

                _context.Catalogs.Add(catalog);
                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    var toReturn = _mapper.Map<Catalog, CatlogDto>(catalog);
                    return toReturn;
                }
                //return Unit.Value;

                //var toReturn = _mapper.Map<Catalog, CatlogDto>(catalog);
                //return toReturn;

                throw new Exception("Problem saving changes");
            }
        }
    }
}