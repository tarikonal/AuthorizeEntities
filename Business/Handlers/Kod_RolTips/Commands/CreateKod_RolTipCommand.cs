
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
using Business.Handlers.Kod_RolTips.ValidationRules;

namespace Business.Handlers.Kod_RolTips.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateKod_RolTipCommand : IRequest<IResult>
    {



        public class CreateKod_RolTipCommandHandler : IRequestHandler<CreateKod_RolTipCommand, IResult>
        {
            private readonly IKod_RolTipRepository _kod_RolTipRepository;
            private readonly IMediator _mediator;
            public CreateKod_RolTipCommandHandler(IKod_RolTipRepository kod_RolTipRepository, IMediator mediator)
            {
                _kod_RolTipRepository = kod_RolTipRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateKod_RolTipValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateKod_RolTipCommand request, CancellationToken cancellationToken)
            {
                var isThereKod_RolTipRecord = _kod_RolTipRepository.Query().Any(/*u => u.*/);

                if (isThereKod_RolTipRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedKod_RolTip = new Kod_RolTip
                {

                };

                _kod_RolTipRepository.Add(addedKod_RolTip);
                await _kod_RolTipRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}