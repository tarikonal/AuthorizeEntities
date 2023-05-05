
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


namespace Business.Handlers.RolMenuIslevObjes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteRolMenuIslevObjeCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteRolMenuIslevObjeCommandHandler : IRequestHandler<DeleteRolMenuIslevObjeCommand, IResult>
        {
            private readonly IRolMenuIslevObjeRepository _rolMenuIslevObjeRepository;
            private readonly IMediator _mediator;

            public DeleteRolMenuIslevObjeCommandHandler(IRolMenuIslevObjeRepository rolMenuIslevObjeRepository, IMediator mediator)
            {
                _rolMenuIslevObjeRepository = rolMenuIslevObjeRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteRolMenuIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var rolMenuIslevObjeToDelete = _rolMenuIslevObjeRepository.Get(p => p.Id == request.Id);

                _rolMenuIslevObjeRepository.Delete(rolMenuIslevObjeToDelete);
                await _rolMenuIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

