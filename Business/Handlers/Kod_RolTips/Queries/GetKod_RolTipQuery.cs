
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Kod_RolTips.Queries
{
    public class GetKod_RolTipQuery : IRequest<IDataResult<Kod_RolTip>>
    {
        public long Id { get; set; }

        public class GetKod_RolTipQueryHandler : IRequestHandler<GetKod_RolTipQuery, IDataResult<Kod_RolTip>>
        {
            private readonly IKod_RolTipRepository _kod_RolTipRepository;
            private readonly IMediator _mediator;

            public GetKod_RolTipQueryHandler(IKod_RolTipRepository kod_RolTipRepository, IMediator mediator)
            {
                _kod_RolTipRepository = kod_RolTipRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Kod_RolTip>> Handle(GetKod_RolTipQuery request, CancellationToken cancellationToken)
            {
                var kod_RolTip = await _kod_RolTipRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Kod_RolTip>(kod_RolTip);
            }
        }
    }
}
