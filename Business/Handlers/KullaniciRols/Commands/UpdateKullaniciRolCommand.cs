
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
using Business.Handlers.KullaniciRols.ValidationRules;


namespace Business.Handlers.KullaniciRols.Commands
{


    public class UpdateKullaniciRolCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public int? KRMKLNKOD { get; set; }
        public long? RolId { get; set; }
        public long? BirlikId { get; set; }
        public int? IDRBRMKOD { get; set; }
        public bool? Durum { get; set; }

        public class UpdateKullaniciRolCommandHandler : IRequestHandler<UpdateKullaniciRolCommand, IResult>
        {
            private readonly IKullaniciRolRepository _kullaniciRolRepository;
            private readonly IMediator _mediator;

            public UpdateKullaniciRolCommandHandler(IKullaniciRolRepository kullaniciRolRepository, IMediator mediator)
            {
                _kullaniciRolRepository = kullaniciRolRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateKullaniciRolValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateKullaniciRolCommand request, CancellationToken cancellationToken)
            {
                var isThereKullaniciRolRecord = await _kullaniciRolRepository.GetAsync(u => u.Id == request.Id);


                isThereKullaniciRolRecord.KRMKLNKOD = request.KRMKLNKOD;
                isThereKullaniciRolRecord.RolId = request.RolId;
                isThereKullaniciRolRecord.BirlikId = request.BirlikId;
                isThereKullaniciRolRecord.IDRBRMKOD = request.IDRBRMKOD;
                isThereKullaniciRolRecord.Durum = request.Durum;


                _kullaniciRolRepository.Update(isThereKullaniciRolRecord);
                await _kullaniciRolRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

