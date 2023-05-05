
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
using Business.Handlers.Yetkis.ValidationRules;


namespace Business.Handlers.Yetkis.Commands
{


    public class UpdateYetkiCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public string YetkiAdi { get; set; }
        public string Aciklama { get; set; }
        public bool? Durum { get; set; }
        public long? ProjeId { get; set; }

        public class UpdateYetkiCommandHandler : IRequestHandler<UpdateYetkiCommand, IResult>
        {
            private readonly IYetkiRepository _yetkiRepository;
            private readonly IMediator _mediator;

            public UpdateYetkiCommandHandler(IYetkiRepository yetkiRepository, IMediator mediator)
            {
                _yetkiRepository = yetkiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateYetkiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateYetkiCommand request, CancellationToken cancellationToken)
            {
                var isThereYetkiRecord = await _yetkiRepository.GetAsync(u => u.Id == request.Id);


                isThereYetkiRecord.YetkiAdi = request.YetkiAdi;
                isThereYetkiRecord.Aciklama = request.Aciklama;
                isThereYetkiRecord.Durum = request.Durum;
                isThereYetkiRecord.ProjeId = request.ProjeId;


                _yetkiRepository.Update(isThereYetkiRecord);
                await _yetkiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

