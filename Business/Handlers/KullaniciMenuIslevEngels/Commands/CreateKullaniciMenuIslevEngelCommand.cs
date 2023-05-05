
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
using Business.Handlers.KullaniciMenuIslevEngels.ValidationRules;

namespace Business.Handlers.KullaniciMenuIslevEngels.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateKullaniciMenuIslevEngelCommand : IRequest<IResult>
    {

        public long MenuId { get; set; }
        public long? IslevId { get; set; }
        public int KRMKLNKOD { get; set; }
        public bool Durum { get; set; }


        public class CreateKullaniciMenuIslevEngelCommandHandler : IRequestHandler<CreateKullaniciMenuIslevEngelCommand, IResult>
        {
            private readonly IKullaniciMenuIslevEngelRepository _kullaniciMenuIslevEngelRepository;
            private readonly IMediator _mediator;
            public CreateKullaniciMenuIslevEngelCommandHandler(IKullaniciMenuIslevEngelRepository kullaniciMenuIslevEngelRepository, IMediator mediator)
            {
                _kullaniciMenuIslevEngelRepository = kullaniciMenuIslevEngelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateKullaniciMenuIslevEngelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateKullaniciMenuIslevEngelCommand request, CancellationToken cancellationToken)
            {
                var isThereKullaniciMenuIslevEngelRecord = _kullaniciMenuIslevEngelRepository.Query().Any(u => u.MenuId == request.MenuId);

                if (isThereKullaniciMenuIslevEngelRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedKullaniciMenuIslevEngel = new KullaniciMenuIslevEngel
                {
                    MenuId = request.MenuId,
                    IslevId = request.IslevId,
                    KRMKLNKOD = request.KRMKLNKOD,
                    Durum = request.Durum,

                };

                _kullaniciMenuIslevEngelRepository.Add(addedKullaniciMenuIslevEngel);
                await _kullaniciMenuIslevEngelRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}