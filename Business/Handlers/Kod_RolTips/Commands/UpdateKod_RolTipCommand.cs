
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
using Business.Handlers.Kod_RolTips.ValidationRules;


namespace Business.Handlers.Kod_RolTips.Commands
{


    public class UpdateKod_RolTipCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class UpdateKod_RolTipCommandHandler : IRequestHandler<UpdateKod_RolTipCommand, IResult>
        {
            private readonly IKod_RolTipRepository _kod_RolTipRepository;
            private readonly IMediator _mediator;

            public UpdateKod_RolTipCommandHandler(IKod_RolTipRepository kod_RolTipRepository, IMediator mediator)
            {
                _kod_RolTipRepository = kod_RolTipRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateKod_RolTipValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateKod_RolTipCommand request, CancellationToken cancellationToken)
            {
                var isThereKod_RolTipRecord = await _kod_RolTipRepository.GetAsync(u => u.Id == request.Id);




                _kod_RolTipRepository.Update(isThereKod_RolTipRecord);
                await _kod_RolTipRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

