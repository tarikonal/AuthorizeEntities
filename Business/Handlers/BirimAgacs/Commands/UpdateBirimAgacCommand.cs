
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
using Business.Handlers.BirimAgacs.ValidationRules;


namespace Business.Handlers.BirimAgacs.Commands
{


    public class UpdateBirimAgacCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public long? BirlikId { get; set; }
        public long? BirimId { get; set; }
        public bool? UyaptaGorunmeDurumu { get; set; }
        public bool? Durum { get; set; }

        public class UpdateBirimAgacCommandHandler : IRequestHandler<UpdateBirimAgacCommand, IResult>
        {
            private readonly IBirimAgacRepository _birimAgacRepository;
            private readonly IMediator _mediator;

            public UpdateBirimAgacCommandHandler(IBirimAgacRepository birimAgacRepository, IMediator mediator)
            {
                _birimAgacRepository = birimAgacRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateBirimAgacValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateBirimAgacCommand request, CancellationToken cancellationToken)
            {
                var isThereBirimAgacRecord = await _birimAgacRepository.GetAsync(u => u.Id == request.Id);


                isThereBirimAgacRecord.BirlikId = request.BirlikId;
                isThereBirimAgacRecord.BirimId = request.BirimId;
                isThereBirimAgacRecord.UyaptaGorunmeDurumu = request.UyaptaGorunmeDurumu;
                isThereBirimAgacRecord.Durum = request.Durum;


                _birimAgacRepository.Update(isThereBirimAgacRecord);
                await _birimAgacRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

