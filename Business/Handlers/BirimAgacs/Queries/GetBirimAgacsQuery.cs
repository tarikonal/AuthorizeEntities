
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

namespace Business.Handlers.BirimAgacs.Queries
{

    public class GetBirimAgacsQuery : IRequest<IDataResult<IEnumerable<BirimAgac>>>
    {
        public class GetBirimAgacsQueryHandler : IRequestHandler<GetBirimAgacsQuery, IDataResult<IEnumerable<BirimAgac>>>
        {
            private readonly IBirimAgacRepository _birimAgacRepository;
            private readonly IMediator _mediator;

            public GetBirimAgacsQueryHandler(IBirimAgacRepository birimAgacRepository, IMediator mediator)
            {
                _birimAgacRepository = birimAgacRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<BirimAgac>>> Handle(GetBirimAgacsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<BirimAgac>>(await _birimAgacRepository.GetListAsync());
            }
        }
    }
}