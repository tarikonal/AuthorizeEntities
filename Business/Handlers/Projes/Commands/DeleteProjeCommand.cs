
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


namespace Business.Handlers.Projes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteProjeCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteProjeCommandHandler : IRequestHandler<DeleteProjeCommand, IResult>
        {
            private readonly IProjeRepository _projeRepository;
            private readonly IMediator _mediator;

            public DeleteProjeCommandHandler(IProjeRepository projeRepository, IMediator mediator)
            {
                _projeRepository = projeRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteProjeCommand request, CancellationToken cancellationToken)
            {
                var projeToDelete = _projeRepository.Get(p => p.Id == request.Id);

                _projeRepository.Delete(projeToDelete);
                await _projeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

