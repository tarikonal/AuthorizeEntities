
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


namespace Business.Handlers.KullaniciMenuIslevObjes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteKullaniciMenuIslevObjeCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteKullaniciMenuIslevObjeCommandHandler : IRequestHandler<DeleteKullaniciMenuIslevObjeCommand, IResult>
        {
            private readonly IKullaniciMenuIslevObjeRepository _kullaniciMenuIslevObjeRepository;
            private readonly IMediator _mediator;

            public DeleteKullaniciMenuIslevObjeCommandHandler(IKullaniciMenuIslevObjeRepository kullaniciMenuIslevObjeRepository, IMediator mediator)
            {
                _kullaniciMenuIslevObjeRepository = kullaniciMenuIslevObjeRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteKullaniciMenuIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var kullaniciMenuIslevObjeToDelete = _kullaniciMenuIslevObjeRepository.Get(p => p.Id == request.Id);

                _kullaniciMenuIslevObjeRepository.Delete(kullaniciMenuIslevObjeToDelete);
                await _kullaniciMenuIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

