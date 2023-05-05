
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
using Business.Handlers.Kod_RolSeviyes.ValidationRules;


namespace Business.Handlers.Kod_RolSeviyes.Commands
{


    public class UpdateKod_RolSeviyeCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public int SeviyeKodu { get; set; }

        public class UpdateKod_RolSeviyeCommandHandler : IRequestHandler<UpdateKod_RolSeviyeCommand, IResult>
        {
            private readonly IKod_RolSeviyeRepository _kod_RolSeviyeRepository;
            private readonly IMediator _mediator;

            public UpdateKod_RolSeviyeCommandHandler(IKod_RolSeviyeRepository kod_RolSeviyeRepository, IMediator mediator)
            {
                _kod_RolSeviyeRepository = kod_RolSeviyeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateKod_RolSeviyeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateKod_RolSeviyeCommand request, CancellationToken cancellationToken)
            {
                var isThereKod_RolSeviyeRecord = await _kod_RolSeviyeRepository.GetAsync(u => u.Id == request.Id);


                isThereKod_RolSeviyeRecord.SeviyeKodu = request.SeviyeKodu;


                _kod_RolSeviyeRepository.Update(isThereKod_RolSeviyeRecord);
                await _kod_RolSeviyeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

