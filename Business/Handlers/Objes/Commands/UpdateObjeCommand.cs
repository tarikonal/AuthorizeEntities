
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
using Business.Handlers.Objes.ValidationRules;


namespace Business.Handlers.Objes.Commands
{


    public class UpdateObjeCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public string ObjeAdi { get; set; }
        public string Aciklama { get; set; }
        public bool? Durum { get; set; }
        public long? ProjeId { get; set; }

        public class UpdateObjeCommandHandler : IRequestHandler<UpdateObjeCommand, IResult>
        {
            private readonly IObjeRepository _objeRepository;
            private readonly IMediator _mediator;

            public UpdateObjeCommandHandler(IObjeRepository objeRepository, IMediator mediator)
            {
                _objeRepository = objeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateObjeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateObjeCommand request, CancellationToken cancellationToken)
            {
                var isThereObjeRecord = await _objeRepository.GetAsync(u => u.Id == request.Id);


                isThereObjeRecord.ObjeAdi = request.ObjeAdi;
                isThereObjeRecord.Aciklama = request.Aciklama;
                isThereObjeRecord.Durum = request.Durum;
                isThereObjeRecord.ProjeId = request.ProjeId;


                _objeRepository.Update(isThereObjeRecord);
                await _objeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

