
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

namespace Business.Handlers.Kod_RolTips.Queries
{

    public class GetKod_RolTipsQuery : IRequest<IDataResult<IEnumerable<Kod_RolTip>>>
    {
        public class GetKod_RolTipsQueryHandler : IRequestHandler<GetKod_RolTipsQuery, IDataResult<IEnumerable<Kod_RolTip>>>
        {
            private readonly IKod_RolTipRepository _kod_RolTipRepository;
            private readonly IMediator _mediator;

            public GetKod_RolTipsQueryHandler(IKod_RolTipRepository kod_RolTipRepository, IMediator mediator)
            {
                _kod_RolTipRepository = kod_RolTipRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Kod_RolTip>>> Handle(GetKod_RolTipsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Kod_RolTip>>(await _kod_RolTipRepository.GetListAsync());
            }
        }
    }
}