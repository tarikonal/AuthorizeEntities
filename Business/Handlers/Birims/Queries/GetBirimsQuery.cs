
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Birims.Queries
{

    public class GetBirimsQuery : IRequest<IDataResult<IEnumerable<Birim>>>
    {
        public class GetBirimsQueryHandler : IRequestHandler<GetBirimsQuery, IDataResult<IEnumerable<Birim>>>
        {
            private readonly IBirimRepository _birimRepository;
            private readonly IMediator _mediator;

            public GetBirimsQueryHandler(IBirimRepository birimRepository, IMediator mediator)
            {
                _birimRepository = birimRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Birim>>> Handle(GetBirimsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Birim>>(await _birimRepository.GetListAsync());
            }
        }
    }
}