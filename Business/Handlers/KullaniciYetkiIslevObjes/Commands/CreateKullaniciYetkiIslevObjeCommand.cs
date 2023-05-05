
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
using Business.Handlers.KullaniciYetkiIslevObjes.ValidationRules;

namespace Business.Handlers.KullaniciYetkiIslevObjes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateKullaniciYetkiIslevObjeCommand : IRequest<IResult>
    {

        public long? KRMKLNKOD { get; set; }
        public long? YetkiId { get; set; }
        public long? IslevId { get; set; }
        public long? ObjeId { get; set; }
        public bool? Durum { get; set; }


        public class CreateKullaniciYetkiIslevObjeCommandHandler : IRequestHandler<CreateKullaniciYetkiIslevObjeCommand, IResult>
        {
            private readonly IKullaniciYetkiIslevObjeRepository _kullaniciYetkiIslevObjeRepository;
            private readonly IMediator _mediator;
            public CreateKullaniciYetkiIslevObjeCommandHandler(IKullaniciYetkiIslevObjeRepository kullaniciYetkiIslevObjeRepository, IMediator mediator)
            {
                _kullaniciYetkiIslevObjeRepository = kullaniciYetkiIslevObjeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateKullaniciYetkiIslevObjeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateKullaniciYetkiIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var isThereKullaniciYetkiIslevObjeRecord = _kullaniciYetkiIslevObjeRepository.Query().Any(u => u.KRMKLNKOD == request.KRMKLNKOD);

                if (isThereKullaniciYetkiIslevObjeRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedKullaniciYetkiIslevObje = new KullaniciYetkiIslevObje
                {
                    KRMKLNKOD = request.KRMKLNKOD,
                    YetkiId = request.YetkiId,
                    IslevId = request.IslevId,
                    ObjeId = request.ObjeId,
                    Durum = request.Durum,

                };

                _kullaniciYetkiIslevObjeRepository.Add(addedKullaniciYetkiIslevObje);
                await _kullaniciYetkiIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}