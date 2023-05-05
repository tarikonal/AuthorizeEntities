
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
using Business.Handlers.KullaniciRols.ValidationRules;

namespace Business.Handlers.KullaniciRols.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateKullaniciRolCommand : IRequest<IResult>
    {

        public int? KRMKLNKOD { get; set; }
        public long? RolId { get; set; }
        public long? BirlikId { get; set; }
        public int? IDRBRMKOD { get; set; }
        public bool? Durum { get; set; }


        public class CreateKullaniciRolCommandHandler : IRequestHandler<CreateKullaniciRolCommand, IResult>
        {
            private readonly IKullaniciRolRepository _kullaniciRolRepository;
            private readonly IMediator _mediator;
            public CreateKullaniciRolCommandHandler(IKullaniciRolRepository kullaniciRolRepository, IMediator mediator)
            {
                _kullaniciRolRepository = kullaniciRolRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateKullaniciRolValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateKullaniciRolCommand request, CancellationToken cancellationToken)
            {
                var isThereKullaniciRolRecord = _kullaniciRolRepository.Query().Any(u => u.KRMKLNKOD == request.KRMKLNKOD);

                if (isThereKullaniciRolRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedKullaniciRol = new KullaniciRol
                {
                    KRMKLNKOD = request.KRMKLNKOD,
                    RolId = request.RolId,
                    BirlikId = request.BirlikId,
                    IDRBRMKOD = request.IDRBRMKOD,
                    Durum = request.Durum,

                };

                _kullaniciRolRepository.Add(addedKullaniciRol);
                await _kullaniciRolRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}