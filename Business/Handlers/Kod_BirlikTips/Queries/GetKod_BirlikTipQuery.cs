
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Kod_BirlikTips.Queries
{
    public class GetKod_BirlikTipQuery : IRequest<IDataResult<Kod_BirlikTip>>
    {
        public long Id { get; set; }

        public class GetKod_BirlikTipQueryHandler : IRequestHandler<GetKod_BirlikTipQuery, IDataResult<Kod_BirlikTip>>
        {
            private readonly IKod_BirlikTipRepository _kod_BirlikTipRepository;
            private readonly IMediator _mediator;

            public GetKod_BirlikTipQueryHandler(IKod_BirlikTipRepository kod_BirlikTipRepository, IMediator mediator)
            {
                _kod_BirlikTipRepository = kod_BirlikTipRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Kod_BirlikTip>> Handle(GetKod_BirlikTipQuery request, CancellationToken cancellationToken)
            {
                var kod_BirlikTip = await _kod_BirlikTipRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Kod_BirlikTip>(kod_BirlikTip);
            }
        }
    }
}
