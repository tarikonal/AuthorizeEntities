
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


namespace Business.Handlers.BirimAgacs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteBirimAgacCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteBirimAgacCommandHandler : IRequestHandler<DeleteBirimAgacCommand, IResult>
        {
            private readonly IBirimAgacRepository _birimAgacRepository;
            private readonly IMediator _mediator;

            public DeleteBirimAgacCommandHandler(IBirimAgacRepository birimAgacRepository, IMediator mediator)
            {
                _birimAgacRepository = birimAgacRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteBirimAgacCommand request, CancellationToken cancellationToken)
            {
                var birimAgacToDelete = _birimAgacRepository.Get(p => p.Id == request.Id);

                _birimAgacRepository.Delete(birimAgacToDelete);
                await _birimAgacRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

