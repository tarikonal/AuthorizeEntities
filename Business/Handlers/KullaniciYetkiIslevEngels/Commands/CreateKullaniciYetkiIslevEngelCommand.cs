
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
using Business.Handlers.KullaniciYetkiIslevEngels.ValidationRules;

namespace Business.Handlers.KullaniciYetkiIslevEngels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateKullaniciYetkiIslevEngelCommand : IRequest<IResult>
    {

        public long YetkiId { get; set; }
        public long? IslevId { get; set; }
        public int KRMKLNKOD { get; set; }
        public bool Durum { get; set; }


        public class CreateKullaniciYetkiIslevEngelCommandHandler : IRequestHandler<CreateKullaniciYetkiIslevEngelCommand, IResult>
        {
            private readonly IKullaniciYetkiIslevEngelRepository _kullaniciYetkiIslevEngelRepository;
            private readonly IMediator _mediator;
            public CreateKullaniciYetkiIslevEngelCommandHandler(IKullaniciYetkiIslevEngelRepository kullaniciYetkiIslevEngelRepository, IMediator mediator)
            {
                _kullaniciYetkiIslevEngelRepository = kullaniciYetkiIslevEngelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateKullaniciYetkiIslevEngelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateKullaniciYetkiIslevEngelCommand request, CancellationToken cancellationToken)
            {
                var isThereKullaniciYetkiIslevEngelRecord = _kullaniciYetkiIslevEngelRepository.Query().Any(u => u.YetkiId == request.YetkiId);

                if (isThereKullaniciYetkiIslevEngelRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedKullaniciYetkiIslevEngel = new KullaniciYetkiIslevEngel
                {
                    YetkiId = request.YetkiId,
                    IslevId = request.IslevId,
                    KRMKLNKOD = request.KRMKLNKOD,
                    Durum = request.Durum,

                };

                _kullaniciYetkiIslevEngelRepository.Add(addedKullaniciYetkiIslevEngel);
                await _kullaniciYetkiIslevEngelRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}