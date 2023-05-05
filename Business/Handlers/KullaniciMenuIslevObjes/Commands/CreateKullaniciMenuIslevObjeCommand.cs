
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
using Business.Handlers.KullaniciMenuIslevObjes.ValidationRules;

namespace Business.Handlers.KullaniciMenuIslevObjes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateKullaniciMenuIslevObjeCommand : IRequest<IResult>
    {

        public long? KRMKLNKOD { get; set; }
        public long? MenuId { get; set; }
        public long? IslevId { get; set; }
        public long? ObjeId { get; set; }
        public bool? Durum { get; set; }


        public class CreateKullaniciMenuIslevObjeCommandHandler : IRequestHandler<CreateKullaniciMenuIslevObjeCommand, IResult>
        {
            private readonly IKullaniciMenuIslevObjeRepository _kullaniciMenuIslevObjeRepository;
            private readonly IMediator _mediator;
            public CreateKullaniciMenuIslevObjeCommandHandler(IKullaniciMenuIslevObjeRepository kullaniciMenuIslevObjeRepository, IMediator mediator)
            {
                _kullaniciMenuIslevObjeRepository = kullaniciMenuIslevObjeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateKullaniciMenuIslevObjeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateKullaniciMenuIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var isThereKullaniciMenuIslevObjeRecord = _kullaniciMenuIslevObjeRepository.Query().Any(u => u.KRMKLNKOD == request.KRMKLNKOD);

                if (isThereKullaniciMenuIslevObjeRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedKullaniciMenuIslevObje = new KullaniciMenuIslevObje
                {
                    KRMKLNKOD = request.KRMKLNKOD,
                    MenuId = request.MenuId,
                    IslevId = request.IslevId,
                    ObjeId = request.ObjeId,
                    Durum = request.Durum,

                };

                _kullaniciMenuIslevObjeRepository.Add(addedKullaniciMenuIslevObje);
                await _kullaniciMenuIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}