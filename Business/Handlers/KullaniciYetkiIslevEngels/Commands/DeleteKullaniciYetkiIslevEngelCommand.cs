
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


namespace Business.Handlers.KullaniciYetkiIslevEngels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteKullaniciYetkiIslevEngelCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteKullaniciYetkiIslevEngelCommandHandler : IRequestHandler<DeleteKullaniciYetkiIslevEngelCommand, IResult>
        {
            private readonly IKullaniciYetkiIslevEngelRepository _kullaniciYetkiIslevEngelRepository;
            private readonly IMediator _mediator;

            public DeleteKullaniciYetkiIslevEngelCommandHandler(IKullaniciYetkiIslevEngelRepository kullaniciYetkiIslevEngelRepository, IMediator mediator)
            {
                _kullaniciYetkiIslevEngelRepository = kullaniciYetkiIslevEngelRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteKullaniciYetkiIslevEngelCommand request, CancellationToken cancellationToken)
            {
                var kullaniciYetkiIslevEngelToDelete = _kullaniciYetkiIslevEngelRepository.Get(p => p.Id == request.Id);

                _kullaniciYetkiIslevEngelRepository.Delete(kullaniciYetkiIslevEngelToDelete);
                await _kullaniciYetkiIslevEngelRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

