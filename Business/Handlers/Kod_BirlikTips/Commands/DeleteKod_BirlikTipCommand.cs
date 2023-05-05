
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Kod_BirlikTips.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteKod_BirlikTipCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteKod_BirlikTipCommandHandler : IRequestHandler<DeleteKod_BirlikTipCommand, IResult>
        {
            private readonly IKod_BirlikTipRepository _kod_BirlikTipRepository;
            private readonly IMediator _mediator;

            public DeleteKod_BirlikTipCommandHandler(IKod_BirlikTipRepository kod_BirlikTipRepository, IMediator mediator)
            {
                _kod_BirlikTipRepository = kod_BirlikTipRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteKod_BirlikTipCommand request, CancellationToken cancellationToken)
            {
                var kod_BirlikTipToDelete = _kod_BirlikTipRepository.Get(p => p.Id == request.Id);

                _kod_BirlikTipRepository.Delete(kod_BirlikTipToDelete);
                await _kod_BirlikTipRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

