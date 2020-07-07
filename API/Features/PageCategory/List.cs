using System;
using System.Threading;
using MediatR;
using System.Threading.Tasks;
using API.Data;
using API.Model;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Features.PageCategory
{
    public class List
    {
        public class Query : IRequest<List<PageItemCategoryDto>> { }

    
        public class Handler : IRequestHandler<Query, List<PageItemCategoryDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                 _mapper = mapper;
            }

            public async Task<List<PageItemCategoryDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var pageItemCategorys = await _context.PageItemCategorys
                    .ToListAsync();
                
                 return _mapper.Map<List<PageItemCategory>, List<PageItemCategoryDto>>(pageItemCategorys);
            }
        }
            
    }
}


