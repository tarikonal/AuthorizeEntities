
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


namespace Business.Handlers.Kod_RolTips.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteKod_RolTipCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteKod_RolTipCommandHandler : IRequestHandler<DeleteKod_RolTipCommand, IResult>
        {
            private readonly IKod_RolTipRepository _kod_RolTipRepository;
            private readonly IMediator _mediator;

            public DeleteKod_RolTipCommandHandler(IKod_RolTipRepository kod_RolTipRepository, IMediator mediator)
            {
                _kod_RolTipRepository = kod_RolTipRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteKod_RolTipCommand request, CancellationToken cancellationToken)
            {
                var kod_RolTipToDelete = _kod_RolTipRepository.Get(p => p.Id == request.Id);

                _kod_RolTipRepository.Delete(kod_RolTipToDelete);
                await _kod_RolTipRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

