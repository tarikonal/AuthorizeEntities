
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
using Business.Handlers.KullaniciYetkiIslevEngels.ValidationRules;


namespace Business.Handlers.KullaniciYetkiIslevEngels.Commands
{


    public class UpdateKullaniciYetkiIslevEngelCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public long YetkiId { get; set; }
        public long? IslevId { get; set; }
        public int KRMKLNKOD { get; set; }
        public bool Durum { get; set; }

        public class UpdateKullaniciYetkiIslevEngelCommandHandler : IRequestHandler<UpdateKullaniciYetkiIslevEngelCommand, IResult>
        {
            private readonly IKullaniciYetkiIslevEngelRepository _kullaniciYetkiIslevEngelRepository;
            private readonly IMediator _mediator;

            public UpdateKullaniciYetkiIslevEngelCommandHandler(IKullaniciYetkiIslevEngelRepository kullaniciYetkiIslevEngelRepository, IMediator mediator)
            {
                _kullaniciYetkiIslevEngelRepository = kullaniciYetkiIslevEngelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateKullaniciYetkiIslevEngelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateKullaniciYetkiIslevEngelCommand request, CancellationToken cancellationToken)
            {
                var isThereKullaniciYetkiIslevEngelRecord = await _kullaniciYetkiIslevEngelRepository.GetAsync(u => u.Id == request.Id);


                isThereKullaniciYetkiIslevEngelRecord.YetkiId = request.YetkiId;
                isThereKullaniciYetkiIslevEngelRecord.IslevId = request.IslevId;
                isThereKullaniciYetkiIslevEngelRecord.KRMKLNKOD = request.KRMKLNKOD;
                isThereKullaniciYetkiIslevEngelRecord.Durum = request.Durum;


                _kullaniciYetkiIslevEngelRepository.Update(isThereKullaniciYetkiIslevEngelRecord);
                await _kullaniciYetkiIslevEngelRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

