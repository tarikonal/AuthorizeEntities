
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
using Business.Handlers.BirimAgacKullaniciRols.ValidationRules;

namespace Business.Handlers.BirimAgacKullaniciRols.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateBirimAgacKullaniciRolCommand : IRequest<IResult>
    {

        public long? BirimAgacId { get; set; }
        public int? KRMKLNKOD { get; set; }
        public long? RolId { get; set; }
        public bool? SureliGorevlendirme { get; set; }
        public System.DateTime? GorevBaslangicTarihi { get; set; }
        public System.DateTime? GorevBitisTarihi { get; set; }
        public bool? Durum { get; set; }


        public class CreateBirimAgacKullaniciRolCommandHandler : IRequestHandler<CreateBirimAgacKullaniciRolCommand, IResult>
        {
            private readonly IBirimAgacKullaniciRolRepository _birimAgacKullaniciRolRepository;
            private readonly IMediator _mediator;
            public CreateBirimAgacKullaniciRolCommandHandler(IBirimAgacKullaniciRolRepository birimAgacKullaniciRolRepository, IMediator mediator)
            {
                _birimAgacKullaniciRolRepository = birimAgacKullaniciRolRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateBirimAgacKullaniciRolValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateBirimAgacKullaniciRolCommand request, CancellationToken cancellationToken)
            {
                var isThereBirimAgacKullaniciRolRecord = _birimAgacKullaniciRolRepository.Query().Any(u => u.BirimAgacId == request.BirimAgacId);

                if (isThereBirimAgacKullaniciRolRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedBirimAgacKullaniciRol = new BirimAgacKullaniciRol
                {
                    BirimAgacId = request.BirimAgacId,
                    KRMKLNKOD = request.KRMKLNKOD,
                    RolId = request.RolId,
                    SureliGorevlendirme = request.SureliGorevlendirme,
                    GorevBaslangicTarihi = request.GorevBaslangicTarihi,
                    GorevBitisTarihi = request.GorevBitisTarihi,
                    Durum = request.Durum,

                };

                _birimAgacKullaniciRolRepository.Add(addedBirimAgacKullaniciRol);
                await _birimAgacKullaniciRolRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}