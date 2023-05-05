
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.RolYetkiIslevObjes.ValidationRules;


namespace Business.Handlers.RolYetkiIslevObjes.Commands
{


    public class UpdateRolYetkiIslevObjeCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public long? RolId { get; set; }
        public long? YetkiId { get; set; }
        public long? IslevId { get; set; }
        public long? ObjeId { get; set; }
        public bool? Durum { get; set; }

        public class UpdateRolYetkiIslevObjeCommandHandler : IRequestHandler<UpdateRolYetkiIslevObjeCommand, IResult>
        {
            private readonly IRolYetkiIslevObjeRepository _rolYetkiIslevObjeRepository;
            private readonly IMediator _mediator;

            public UpdateRolYetkiIslevObjeCommandHandler(IRolYetkiIslevObjeRepository rolYetkiIslevObjeRepository, IMediator mediator)
            {
                _rolYetkiIslevObjeRepository = rolYetkiIslevObjeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateRolYetkiIslevObjeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateRolYetkiIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var isThereRolYetkiIslevObjeRecord = await _rolYetkiIslevObjeRepository.GetAsync(u => u.Id == request.Id);


                isThereRolYetkiIslevObjeRecord.RolId = request.RolId;
                isThereRolYetkiIslevObjeRecord.YetkiId = request.YetkiId;
                isThereRolYetkiIslevObjeRecord.IslevId = request.IslevId;
                isThereRolYetkiIslevObjeRecord.ObjeId = request.ObjeId;
                isThereRolYetkiIslevObjeRecord.Durum = request.Durum;


                _rolYetkiIslevObjeRepository.Update(isThereRolYetkiIslevObjeRecord);
                await _rolYetkiIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

