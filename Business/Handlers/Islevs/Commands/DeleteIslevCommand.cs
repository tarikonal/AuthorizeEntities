
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


namespace Business.Handlers.Islevs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteIslevCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteIslevCommandHandler : IRequestHandler<DeleteIslevCommand, IResult>
        {
            private readonly IIslevRepository _islevRepository;
            private readonly IMediator _mediator;

            public DeleteIslevCommandHandler(IIslevRepository islevRepository, IMediator mediator)
            {
                _islevRepository = islevRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteIslevCommand request, CancellationToken cancellationToken)
            {
                var islevToDelete = _islevRepository.Get(p => p.Id == request.Id);

                _islevRepository.Delete(islevToDelete);
                await _islevRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

