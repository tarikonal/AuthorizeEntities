
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


namespace Business.Handlers.RolYetkiIslevObjes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteRolYetkiIslevObjeCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteRolYetkiIslevObjeCommandHandler : IRequestHandler<DeleteRolYetkiIslevObjeCommand, IResult>
        {
            private readonly IRolYetkiIslevObjeRepository _rolYetkiIslevObjeRepository;
            private readonly IMediator _mediator;

            public DeleteRolYetkiIslevObjeCommandHandler(IRolYetkiIslevObjeRepository rolYetkiIslevObjeRepository, IMediator mediator)
            {
                _rolYetkiIslevObjeRepository = rolYetkiIslevObjeRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteRolYetkiIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var rolYetkiIslevObjeToDelete = _rolYetkiIslevObjeRepository.Get(p => p.Id == request.Id);

                _rolYetkiIslevObjeRepository.Delete(rolYetkiIslevObjeToDelete);
                await _rolYetkiIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

