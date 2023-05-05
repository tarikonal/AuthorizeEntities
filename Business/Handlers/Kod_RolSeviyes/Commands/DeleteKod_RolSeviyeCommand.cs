
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


namespace Business.Handlers.Kod_RolSeviyes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteKod_RolSeviyeCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteKod_RolSeviyeCommandHandler : IRequestHandler<DeleteKod_RolSeviyeCommand, IResult>
        {
            private readonly IKod_RolSeviyeRepository _kod_RolSeviyeRepository;
            private readonly IMediator _mediator;

            public DeleteKod_RolSeviyeCommandHandler(IKod_RolSeviyeRepository kod_RolSeviyeRepository, IMediator mediator)
            {
                _kod_RolSeviyeRepository = kod_RolSeviyeRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteKod_RolSeviyeCommand request, CancellationToken cancellationToken)
            {
                var kod_RolSeviyeToDelete = _kod_RolSeviyeRepository.Get(p => p.Id == request.Id);

                _kod_RolSeviyeRepository.Delete(kod_RolSeviyeToDelete);
                await _kod_RolSeviyeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

