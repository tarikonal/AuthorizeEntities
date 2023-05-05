
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


namespace Business.Handlers.BirimYetkiIslevObjes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteBirimYetkiIslevObjeCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteBirimYetkiIslevObjeCommandHandler : IRequestHandler<DeleteBirimYetkiIslevObjeCommand, IResult>
        {
            private readonly IBirimYetkiIslevObjeRepository _birimYetkiIslevObjeRepository;
            private readonly IMediator _mediator;

            public DeleteBirimYetkiIslevObjeCommandHandler(IBirimYetkiIslevObjeRepository birimYetkiIslevObjeRepository, IMediator mediator)
            {
                _birimYetkiIslevObjeRepository = birimYetkiIslevObjeRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteBirimYetkiIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var birimYetkiIslevObjeToDelete = _birimYetkiIslevObjeRepository.Get(p => p.Id == request.Id);

                _birimYetkiIslevObjeRepository.Delete(birimYetkiIslevObjeToDelete);
                await _birimYetkiIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

