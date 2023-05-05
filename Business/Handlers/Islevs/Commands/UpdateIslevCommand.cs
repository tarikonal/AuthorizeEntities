
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
using Business.Handlers.Islevs.ValidationRules;


namespace Business.Handlers.Islevs.Commands
{


    public class UpdateIslevCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public string IslevAdi { get; set; }
        public string Aciklama { get; set; }
        public bool? Durum { get; set; }
        public long? ProjeId { get; set; }

        public class UpdateIslevCommandHandler : IRequestHandler<UpdateIslevCommand, IResult>
        {
            private readonly IIslevRepository _islevRepository;
            private readonly IMediator _mediator;

            public UpdateIslevCommandHandler(IIslevRepository islevRepository, IMediator mediator)
            {
                _islevRepository = islevRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateIslevValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateIslevCommand request, CancellationToken cancellationToken)
            {
                var isThereIslevRecord = await _islevRepository.GetAsync(u => u.Id == request.Id);


                isThereIslevRecord.IslevAdi = request.IslevAdi;
                isThereIslevRecord.Aciklama = request.Aciklama;
                isThereIslevRecord.Durum = request.Durum;
                isThereIslevRecord.ProjeId = request.ProjeId;


                _islevRepository.Update(isThereIslevRecord);
                await _islevRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

