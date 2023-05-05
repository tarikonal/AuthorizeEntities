
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


namespace Business.Handlers.KullaniciYetkiIslevObjes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteKullaniciYetkiIslevObjeCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteKullaniciYetkiIslevObjeCommandHandler : IRequestHandler<DeleteKullaniciYetkiIslevObjeCommand, IResult>
        {
            private readonly IKullaniciYetkiIslevObjeRepository _kullaniciYetkiIslevObjeRepository;
            private readonly IMediator _mediator;

            public DeleteKullaniciYetkiIslevObjeCommandHandler(IKullaniciYetkiIslevObjeRepository kullaniciYetkiIslevObjeRepository, IMediator mediator)
            {
                _kullaniciYetkiIslevObjeRepository = kullaniciYetkiIslevObjeRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteKullaniciYetkiIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var kullaniciYetkiIslevObjeToDelete = _kullaniciYetkiIslevObjeRepository.Get(p => p.Id == request.Id);

                _kullaniciYetkiIslevObjeRepository.Delete(kullaniciYetkiIslevObjeToDelete);
                await _kullaniciYetkiIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

