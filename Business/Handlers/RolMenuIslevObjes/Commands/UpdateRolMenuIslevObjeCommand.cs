
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
using Business.Handlers.RolMenuIslevObjes.ValidationRules;


namespace Business.Handlers.RolMenuIslevObjes.Commands
{


    public class UpdateRolMenuIslevObjeCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public long? RolId { get; set; }
        public long? MenuId { get; set; }
        public long? IslevId { get; set; }
        public long? ObjeId { get; set; }
        public bool? Durum { get; set; }

        public class UpdateRolMenuIslevObjeCommandHandler : IRequestHandler<UpdateRolMenuIslevObjeCommand, IResult>
        {
            private readonly IRolMenuIslevObjeRepository _rolMenuIslevObjeRepository;
            private readonly IMediator _mediator;

            public UpdateRolMenuIslevObjeCommandHandler(IRolMenuIslevObjeRepository rolMenuIslevObjeRepository, IMediator mediator)
            {
                _rolMenuIslevObjeRepository = rolMenuIslevObjeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateRolMenuIslevObjeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateRolMenuIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var isThereRolMenuIslevObjeRecord = await _rolMenuIslevObjeRepository.GetAsync(u => u.Id == request.Id);


                isThereRolMenuIslevObjeRecord.RolId = request.RolId;
                isThereRolMenuIslevObjeRecord.MenuId = request.MenuId;
                isThereRolMenuIslevObjeRecord.IslevId = request.IslevId;
                isThereRolMenuIslevObjeRecord.ObjeId = request.ObjeId;
                isThereRolMenuIslevObjeRecord.Durum = request.Durum;


                _rolMenuIslevObjeRepository.Update(isThereRolMenuIslevObjeRecord);
                await _rolMenuIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

