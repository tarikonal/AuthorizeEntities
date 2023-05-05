
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


namespace Business.Handlers.KullaniciMenuIslevEngels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteKullaniciMenuIslevEngelCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteKullaniciMenuIslevEngelCommandHandler : IRequestHandler<DeleteKullaniciMenuIslevEngelCommand, IResult>
        {
            private readonly IKullaniciMenuIslevEngelRepository _kullaniciMenuIslevEngelRepository;
            private readonly IMediator _mediator;

            public DeleteKullaniciMenuIslevEngelCommandHandler(IKullaniciMenuIslevEngelRepository kullaniciMenuIslevEngelRepository, IMediator mediator)
            {
                _kullaniciMenuIslevEngelRepository = kullaniciMenuIslevEngelRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteKullaniciMenuIslevEngelCommand request, CancellationToken cancellationToken)
            {
                var kullaniciMenuIslevEngelToDelete = _kullaniciMenuIslevEngelRepository.Get(p => p.Id == request.Id);

                _kullaniciMenuIslevEngelRepository.Delete(kullaniciMenuIslevEngelToDelete);
                await _kullaniciMenuIslevEngelRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

