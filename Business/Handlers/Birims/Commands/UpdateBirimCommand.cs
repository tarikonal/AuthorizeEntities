
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
using Business.Handlers.Birims.ValidationRules;


namespace Business.Handlers.Birims.Commands
{


    public class UpdateBirimCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public string KeyValue { get; set; }
        public string BirimAdi { get; set; }
        public long? ProjeId { get; set; }
        public bool? Durum { get; set; }

        public class UpdateBirimCommandHandler : IRequestHandler<UpdateBirimCommand, IResult>
        {
            private readonly IBirimRepository _birimRepository;
            private readonly IMediator _mediator;

            public UpdateBirimCommandHandler(IBirimRepository birimRepository, IMediator mediator)
            {
                _birimRepository = birimRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateBirimValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateBirimCommand request, CancellationToken cancellationToken)
            {
                var isThereBirimRecord = await _birimRepository.GetAsync(u => u.Id == request.Id);


                isThereBirimRecord.KeyValue = request.KeyValue;
                isThereBirimRecord.BirimAdi = request.BirimAdi;
                isThereBirimRecord.ProjeId = request.ProjeId;
                isThereBirimRecord.Durum = request.Durum;


                _birimRepository.Update(isThereBirimRecord);
                await _birimRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

