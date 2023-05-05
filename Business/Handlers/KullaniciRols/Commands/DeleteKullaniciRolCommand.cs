
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


namespace Business.Handlers.KullaniciRols.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteKullaniciRolCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteKullaniciRolCommandHandler : IRequestHandler<DeleteKullaniciRolCommand, IResult>
        {
            private readonly IKullaniciRolRepository _kullaniciRolRepository;
            private readonly IMediator _mediator;

            public DeleteKullaniciRolCommandHandler(IKullaniciRolRepository kullaniciRolRepository, IMediator mediator)
            {
                _kullaniciRolRepository = kullaniciRolRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteKullaniciRolCommand request, CancellationToken cancellationToken)
            {
                var kullaniciRolToDelete = _kullaniciRolRepository.Get(p => p.Id == request.Id);

                _kullaniciRolRepository.Delete(kullaniciRolToDelete);
                await _kullaniciRolRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

