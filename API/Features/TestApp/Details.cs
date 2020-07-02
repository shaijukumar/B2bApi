using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using API.Errors;
using API.Model;
using AutoMapper;
using MediatR;

namespace API.Features._TestApp
{
    public class Details
    {
        public class Query : IRequest<TestAppDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, TestAppDto>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<TestAppDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var testApp = await _context.TestApps
                    .FindAsync(request.Id);

                if (testApp == null)
                    throw new RestException(HttpStatusCode.NotFound, new { TestApp = "Not found" });

                var toReturn = _mapper.Map <TestApp, TestAppDto>(testApp); 

                return toReturn;
            }
    }
}
}
