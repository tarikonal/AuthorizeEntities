
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
using Business.Handlers.Kod_BirlikTips.ValidationRules;


namespace Business.Handlers.Kod_BirlikTips.Commands
{


    public class UpdateKod_BirlikTipCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class UpdateKod_BirlikTipCommandHandler : IRequestHandler<UpdateKod_BirlikTipCommand, IResult>
        {
            private readonly IKod_BirlikTipRepository _kod_BirlikTipRepository;
            private readonly IMediator _mediator;

            public UpdateKod_BirlikTipCommandHandler(IKod_BirlikTipRepository kod_BirlikTipRepository, IMediator mediator)
            {
                _kod_BirlikTipRepository = kod_BirlikTipRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateKod_BirlikTipValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateKod_BirlikTipCommand request, CancellationToken cancellationToken)
            {
                var isThereKod_BirlikTipRecord = await _kod_BirlikTipRepository.GetAsync(u => u.Id == request.Id);




                _kod_BirlikTipRepository.Update(isThereKod_BirlikTipRecord);
                await _kod_BirlikTipRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

