
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
using Business.Handlers.KullaniciYetkiIslevObjes.ValidationRules;


namespace Business.Handlers.KullaniciYetkiIslevObjes.Commands
{


    public class UpdateKullaniciYetkiIslevObjeCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public long? KRMKLNKOD { get; set; }
        public long? YetkiId { get; set; }
        public long? IslevId { get; set; }
        public long? ObjeId { get; set; }
        public bool? Durum { get; set; }

        public class UpdateKullaniciYetkiIslevObjeCommandHandler : IRequestHandler<UpdateKullaniciYetkiIslevObjeCommand, IResult>
        {
            private readonly IKullaniciYetkiIslevObjeRepository _kullaniciYetkiIslevObjeRepository;
            private readonly IMediator _mediator;

            public UpdateKullaniciYetkiIslevObjeCommandHandler(IKullaniciYetkiIslevObjeRepository kullaniciYetkiIslevObjeRepository, IMediator mediator)
            {
                _kullaniciYetkiIslevObjeRepository = kullaniciYetkiIslevObjeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateKullaniciYetkiIslevObjeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateKullaniciYetkiIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var isThereKullaniciYetkiIslevObjeRecord = await _kullaniciYetkiIslevObjeRepository.GetAsync(u => u.Id == request.Id);


                isThereKullaniciYetkiIslevObjeRecord.KRMKLNKOD = request.KRMKLNKOD;
                isThereKullaniciYetkiIslevObjeRecord.YetkiId = request.YetkiId;
                isThereKullaniciYetkiIslevObjeRecord.IslevId = request.IslevId;
                isThereKullaniciYetkiIslevObjeRecord.ObjeId = request.ObjeId;
                isThereKullaniciYetkiIslevObjeRecord.Durum = request.Durum;


                _kullaniciYetkiIslevObjeRepository.Update(isThereKullaniciYetkiIslevObjeRecord);
                await _kullaniciYetkiIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

