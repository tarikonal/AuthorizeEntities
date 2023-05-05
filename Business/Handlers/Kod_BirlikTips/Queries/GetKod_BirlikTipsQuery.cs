
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

namespace Business.Handlers.Kod_BirlikTips.Queries
{

    public class GetKod_BirlikTipsQuery : IRequest<IDataResult<IEnumerable<Kod_BirlikTip>>>
    {
        public class GetKod_BirlikTipsQueryHandler : IRequestHandler<GetKod_BirlikTipsQuery, IDataResult<IEnumerable<Kod_BirlikTip>>>
        {
            private readonly IKod_BirlikTipRepository _kod_BirlikTipRepository;
            private readonly IMediator _mediator;

            public GetKod_BirlikTipsQueryHandler(IKod_BirlikTipRepository kod_BirlikTipRepository, IMediator mediator)
            {
                _kod_BirlikTipRepository = kod_BirlikTipRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Kod_BirlikTip>>> Handle(GetKod_BirlikTipsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Kod_BirlikTip>>(await _kod_BirlikTipRepository.GetListAsync());
            }
        }
    }
}