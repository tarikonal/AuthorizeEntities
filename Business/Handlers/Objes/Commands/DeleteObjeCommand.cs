
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


namespace Business.Handlers.Objes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteObjeCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteObjeCommandHandler : IRequestHandler<DeleteObjeCommand, IResult>
        {
            private readonly IObjeRepository _objeRepository;
            private readonly IMediator _mediator;

            public DeleteObjeCommandHandler(IObjeRepository objeRepository, IMediator mediator)
            {
                _objeRepository = objeRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteObjeCommand request, CancellationToken cancellationToken)
            {
                var objeToDelete = _objeRepository.Get(p => p.Id == request.Id);

                _objeRepository.Delete(objeToDelete);
                await _objeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

