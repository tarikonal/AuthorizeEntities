
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
using Business.Handlers.Birims.ValidationRules;

namespace Business.Handlers.Birims.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateBirimCommand : IRequest<IResult>
    {

        public string KeyValue { get; set; }
        public string BirimAdi { get; set; }
        public long? ProjeId { get; set; }
        public bool? Durum { get; set; }


        public class CreateBirimCommandHandler : IRequestHandler<CreateBirimCommand, IResult>
        {
            private readonly IBirimRepository _birimRepository;
            private readonly IMediator _mediator;
            public CreateBirimCommandHandler(IBirimRepository birimRepository, IMediator mediator)
            {
                _birimRepository = birimRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateBirimValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateBirimCommand request, CancellationToken cancellationToken)
            {
                var isThereBirimRecord = _birimRepository.Query().Any(u => u.KeyValue == request.KeyValue);

                if (isThereBirimRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedBirim = new Birim
                {
                    KeyValue = request.KeyValue,
                    BirimAdi = request.BirimAdi,
                    ProjeId = request.ProjeId,
                    Durum = request.Durum,

                };

                _birimRepository.Add(addedBirim);
                await _birimRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}