
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
using Business.Handlers.KullaniciMenuIslevEngels.ValidationRules;


namespace Business.Handlers.KullaniciMenuIslevEngels.Commands
{


    public class UpdateKullaniciMenuIslevEngelCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        public long? IslevId { get; set; }
        public int KRMKLNKOD { get; set; }
        public bool Durum { get; set; }

        public class UpdateKullaniciMenuIslevEngelCommandHandler : IRequestHandler<UpdateKullaniciMenuIslevEngelCommand, IResult>
        {
            private readonly IKullaniciMenuIslevEngelRepository _kullaniciMenuIslevEngelRepository;
            private readonly IMediator _mediator;

            public UpdateKullaniciMenuIslevEngelCommandHandler(IKullaniciMenuIslevEngelRepository kullaniciMenuIslevEngelRepository, IMediator mediator)
            {
                _kullaniciMenuIslevEngelRepository = kullaniciMenuIslevEngelRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateKullaniciMenuIslevEngelValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateKullaniciMenuIslevEngelCommand request, CancellationToken cancellationToken)
            {
                var isThereKullaniciMenuIslevEngelRecord = await _kullaniciMenuIslevEngelRepository.GetAsync(u => u.Id == request.Id);


                isThereKullaniciMenuIslevEngelRecord.MenuId = request.MenuId;
                isThereKullaniciMenuIslevEngelRecord.IslevId = request.IslevId;
                isThereKullaniciMenuIslevEngelRecord.KRMKLNKOD = request.KRMKLNKOD;
                isThereKullaniciMenuIslevEngelRecord.Durum = request.Durum;


                _kullaniciMenuIslevEngelRepository.Update(isThereKullaniciMenuIslevEngelRecord);
                await _kullaniciMenuIslevEngelRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

