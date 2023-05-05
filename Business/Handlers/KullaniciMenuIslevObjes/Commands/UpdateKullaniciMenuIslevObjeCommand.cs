
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
using Business.Handlers.KullaniciMenuIslevObjes.ValidationRules;


namespace Business.Handlers.KullaniciMenuIslevObjes.Commands
{


    public class UpdateKullaniciMenuIslevObjeCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public long? KRMKLNKOD { get; set; }
        public long? MenuId { get; set; }
        public long? IslevId { get; set; }
        public long? ObjeId { get; set; }
        public bool? Durum { get; set; }

        public class UpdateKullaniciMenuIslevObjeCommandHandler : IRequestHandler<UpdateKullaniciMenuIslevObjeCommand, IResult>
        {
            private readonly IKullaniciMenuIslevObjeRepository _kullaniciMenuIslevObjeRepository;
            private readonly IMediator _mediator;

            public UpdateKullaniciMenuIslevObjeCommandHandler(IKullaniciMenuIslevObjeRepository kullaniciMenuIslevObjeRepository, IMediator mediator)
            {
                _kullaniciMenuIslevObjeRepository = kullaniciMenuIslevObjeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateKullaniciMenuIslevObjeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateKullaniciMenuIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var isThereKullaniciMenuIslevObjeRecord = await _kullaniciMenuIslevObjeRepository.GetAsync(u => u.Id == request.Id);


                isThereKullaniciMenuIslevObjeRecord.KRMKLNKOD = request.KRMKLNKOD;
                isThereKullaniciMenuIslevObjeRecord.MenuId = request.MenuId;
                isThereKullaniciMenuIslevObjeRecord.IslevId = request.IslevId;
                isThereKullaniciMenuIslevObjeRecord.ObjeId = request.ObjeId;
                isThereKullaniciMenuIslevObjeRecord.Durum = request.Durum;


                _kullaniciMenuIslevObjeRepository.Update(isThereKullaniciMenuIslevObjeRecord);
                await _kullaniciMenuIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

