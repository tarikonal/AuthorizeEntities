
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


namespace Business.Handlers.Birims.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteBirimCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteBirimCommandHandler : IRequestHandler<DeleteBirimCommand, IResult>
        {
            private readonly IBirimRepository _birimRepository;
            private readonly IMediator _mediator;

            public DeleteBirimCommandHandler(IBirimRepository birimRepository, IMediator mediator)
            {
                _birimRepository = birimRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteBirimCommand request, CancellationToken cancellationToken)
            {
                var birimToDelete = _birimRepository.Get(p => p.Id == request.Id);

                _birimRepository.Delete(birimToDelete);
                await _birimRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

