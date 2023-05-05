
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
using Business.Handlers.Kod_BirlikTips.ValidationRules;

namespace Business.Handlers.Kod_BirlikTips.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateKod_BirlikTipCommand : IRequest<IResult>
    {



        public class CreateKod_BirlikTipCommandHandler : IRequestHandler<CreateKod_BirlikTipCommand, IResult>
        {
            private readonly IKod_BirlikTipRepository _kod_BirlikTipRepository;
            private readonly IMediator _mediator;
            public CreateKod_BirlikTipCommandHandler(IKod_BirlikTipRepository kod_BirlikTipRepository, IMediator mediator)
            {
                _kod_BirlikTipRepository = kod_BirlikTipRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateKod_BirlikTipValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateKod_BirlikTipCommand request, CancellationToken cancellationToken)
            {
                var isThereKod_BirlikTipRecord = _kod_BirlikTipRepository.Query().Any(/*u => u.*/);

                if (isThereKod_BirlikTipRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedKod_BirlikTip = new Kod_BirlikTip
                {

                };

                _kod_BirlikTipRepository.Add(addedKod_BirlikTip);
                await _kod_BirlikTipRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}