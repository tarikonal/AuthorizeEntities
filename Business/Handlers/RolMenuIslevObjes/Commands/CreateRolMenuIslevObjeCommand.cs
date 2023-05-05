
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.RolMenuIslevObjes.ValidationRules;

namespace Business.Handlers.RolMenuIslevObjes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRolMenuIslevObjeCommand : IRequest<IResult>
    {

        public long? RolId { get; set; }
        public long? MenuId { get; set; }
        public long? IslevId { get; set; }
        public long? ObjeId { get; set; }
        public bool? Durum { get; set; }


        public class CreateRolMenuIslevObjeCommandHandler : IRequestHandler<CreateRolMenuIslevObjeCommand, IResult>
        {
            private readonly IRolMenuIslevObjeRepository _rolMenuIslevObjeRepository;
            private readonly IMediator _mediator;
            public CreateRolMenuIslevObjeCommandHandler(IRolMenuIslevObjeRepository rolMenuIslevObjeRepository, IMediator mediator)
            {
                _rolMenuIslevObjeRepository = rolMenuIslevObjeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateRolMenuIslevObjeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateRolMenuIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var isThereRolMenuIslevObjeRecord = _rolMenuIslevObjeRepository.Query().Any(u => u.RolId == request.RolId);

                if (isThereRolMenuIslevObjeRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedRolMenuIslevObje = new RolMenuIslevObje
                {
                    RolId = request.RolId,
                    MenuId = request.MenuId,
                    IslevId = request.IslevId,
                    ObjeId = request.ObjeId,
                    Durum = request.Durum,

                };

                _rolMenuIslevObjeRepository.Add(addedRolMenuIslevObje);
                await _rolMenuIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}