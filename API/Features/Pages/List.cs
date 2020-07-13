using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using API.Data;
using API.Model;

namespace Application.Pages
{
    public class List
    {
        public class Query : IRequest<List<PageDto>> { }

    
        public class Handler : IRequestHandler<Query, List<PageDto>>
        {
            private readonly DataContext _context;
             private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<PageDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var pages = await _context.Pages
                    .ToListAsync();
                //return pages;
                return _mapper.Map<List<Page>, List<PageDto>>(pages);

            } 
        } 
            
    }
} 

