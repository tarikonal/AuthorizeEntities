
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
using Business.Handlers.BirimAgacKullaniciRols.ValidationRules;


namespace Business.Handlers.BirimAgacKullaniciRols.Commands
{


    public class UpdateBirimAgacKullaniciRolCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public long? BirimAgacId { get; set; }
        public int? KRMKLNKOD { get; set; }
        public long? RolId { get; set; }
        public bool? SureliGorevlendirme { get; set; }
        public System.DateTime? GorevBaslangicTarihi { get; set; }
        public System.DateTime? GorevBitisTarihi { get; set; }
        public bool? Durum { get; set; }

        public class UpdateBirimAgacKullaniciRolCommandHandler : IRequestHandler<UpdateBirimAgacKullaniciRolCommand, IResult>
        {
            private readonly IBirimAgacKullaniciRolRepository _birimAgacKullaniciRolRepository;
            private readonly IMediator _mediator;

            public UpdateBirimAgacKullaniciRolCommandHandler(IBirimAgacKullaniciRolRepository birimAgacKullaniciRolRepository, IMediator mediator)
            {
                _birimAgacKullaniciRolRepository = birimAgacKullaniciRolRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateBirimAgacKullaniciRolValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateBirimAgacKullaniciRolCommand request, CancellationToken cancellationToken)
            {
                var isThereBirimAgacKullaniciRolRecord = await _birimAgacKullaniciRolRepository.GetAsync(u => u.Id == request.Id);


                isThereBirimAgacKullaniciRolRecord.BirimAgacId = request.BirimAgacId;
                isThereBirimAgacKullaniciRolRecord.KRMKLNKOD = request.KRMKLNKOD;
                isThereBirimAgacKullaniciRolRecord.RolId = request.RolId;
                isThereBirimAgacKullaniciRolRecord.SureliGorevlendirme = request.SureliGorevlendirme;
                isThereBirimAgacKullaniciRolRecord.GorevBaslangicTarihi = request.GorevBaslangicTarihi;
                isThereBirimAgacKullaniciRolRecord.GorevBitisTarihi = request.GorevBitisTarihi;
                isThereBirimAgacKullaniciRolRecord.Durum = request.Durum;


                _birimAgacKullaniciRolRepository.Update(isThereBirimAgacKullaniciRolRecord);
                await _birimAgacKullaniciRolRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

