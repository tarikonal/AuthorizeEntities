
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
using Business.Handlers.RolYetkiIslevObjes.ValidationRules;

namespace Business.Handlers.RolYetkiIslevObjes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRolYetkiIslevObjeCommand : IRequest<IResult>
    {

        public long? RolId { get; set; }
        public long? YetkiId { get; set; }
        public long? IslevId { get; set; }
        public long? ObjeId { get; set; }
        public bool? Durum { get; set; }


        public class CreateRolYetkiIslevObjeCommandHandler : IRequestHandler<CreateRolYetkiIslevObjeCommand, IResult>
        {
            private readonly IRolYetkiIslevObjeRepository _rolYetkiIslevObjeRepository;
            private readonly IMediator _mediator;
            public CreateRolYetkiIslevObjeCommandHandler(IRolYetkiIslevObjeRepository rolYetkiIslevObjeRepository, IMediator mediator)
            {
                _rolYetkiIslevObjeRepository = rolYetkiIslevObjeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateRolYetkiIslevObjeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateRolYetkiIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var isThereRolYetkiIslevObjeRecord = _rolYetkiIslevObjeRepository.Query().Any(u => u.RolId == request.RolId);

                if (isThereRolYetkiIslevObjeRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedRolYetkiIslevObje = new RolYetkiIslevObje
                {
                    RolId = request.RolId,
                    YetkiId = request.YetkiId,
                    IslevId = request.IslevId,
                    ObjeId = request.ObjeId,
                    Durum = request.Durum,

                };

                _rolYetkiIslevObjeRepository.Add(addedRolYetkiIslevObje);
                await _rolYetkiIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}