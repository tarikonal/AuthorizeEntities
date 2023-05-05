
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


namespace Business.Handlers.Rols.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteRolCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteRolCommandHandler : IRequestHandler<DeleteRolCommand, IResult>
        {
            private readonly IRolRepository _rolRepository;
            private readonly IMediator _mediator;

            public DeleteRolCommandHandler(IRolRepository rolRepository, IMediator mediator)
            {
                _rolRepository = rolRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteRolCommand request, CancellationToken cancellationToken)
            {
                var rolToDelete = _rolRepository.Get(p => p.Id == request.Id);

                _rolRepository.Delete(rolToDelete);
                await _rolRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

