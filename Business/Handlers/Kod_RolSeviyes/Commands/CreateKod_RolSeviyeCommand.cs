
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
using Business.Handlers.Kod_RolSeviyes.ValidationRules;

namespace Business.Handlers.Kod_RolSeviyes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateKod_RolSeviyeCommand : IRequest<IResult>
    {

        public int SeviyeKodu { get; set; }


        public class CreateKod_RolSeviyeCommandHandler : IRequestHandler<CreateKod_RolSeviyeCommand, IResult>
        {
            private readonly IKod_RolSeviyeRepository _kod_RolSeviyeRepository;
            private readonly IMediator _mediator;
            public CreateKod_RolSeviyeCommandHandler(IKod_RolSeviyeRepository kod_RolSeviyeRepository, IMediator mediator)
            {
                _kod_RolSeviyeRepository = kod_RolSeviyeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateKod_RolSeviyeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateKod_RolSeviyeCommand request, CancellationToken cancellationToken)
            {
                var isThereKod_RolSeviyeRecord = _kod_RolSeviyeRepository.Query().Any(u => u.SeviyeKodu == request.SeviyeKodu);

                if (isThereKod_RolSeviyeRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedKod_RolSeviye = new Kod_RolSeviye
                {
                    SeviyeKodu = request.SeviyeKodu,

                };

                _kod_RolSeviyeRepository.Add(addedKod_RolSeviye);
                await _kod_RolSeviyeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}