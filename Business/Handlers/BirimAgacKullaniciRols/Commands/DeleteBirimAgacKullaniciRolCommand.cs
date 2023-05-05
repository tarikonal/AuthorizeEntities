
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


namespace Business.Handlers.BirimAgacKullaniciRols.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteBirimAgacKullaniciRolCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteBirimAgacKullaniciRolCommandHandler : IRequestHandler<DeleteBirimAgacKullaniciRolCommand, IResult>
        {
            private readonly IBirimAgacKullaniciRolRepository _birimAgacKullaniciRolRepository;
            private readonly IMediator _mediator;

            public DeleteBirimAgacKullaniciRolCommandHandler(IBirimAgacKullaniciRolRepository birimAgacKullaniciRolRepository, IMediator mediator)
            {
                _birimAgacKullaniciRolRepository = birimAgacKullaniciRolRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteBirimAgacKullaniciRolCommand request, CancellationToken cancellationToken)
            {
                var birimAgacKullaniciRolToDelete = _birimAgacKullaniciRolRepository.Get(p => p.Id == request.Id);

                _birimAgacKullaniciRolRepository.Delete(birimAgacKullaniciRolToDelete);
                await _birimAgacKullaniciRolRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

