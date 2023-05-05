
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
using Business.Handlers.BirimAgacs.ValidationRules;

namespace Business.Handlers.BirimAgacs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateBirimAgacCommand : IRequest<IResult>
    {

        public long? BirlikId { get; set; }
        public long? BirimId { get; set; }
        public bool? UyaptaGorunmeDurumu { get; set; }
        public bool? Durum { get; set; }


        public class CreateBirimAgacCommandHandler : IRequestHandler<CreateBirimAgacCommand, IResult>
        {
            private readonly IBirimAgacRepository _birimAgacRepository;
            private readonly IMediator _mediator;
            public CreateBirimAgacCommandHandler(IBirimAgacRepository birimAgacRepository, IMediator mediator)
            {
                _birimAgacRepository = birimAgacRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateBirimAgacValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateBirimAgacCommand request, CancellationToken cancellationToken)
            {
                var isThereBirimAgacRecord = _birimAgacRepository.Query().Any(u => u.BirlikId == request.BirlikId);

                if (isThereBirimAgacRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedBirimAgac = new BirimAgac
                {
                    BirlikId = request.BirlikId,
                    BirimId = request.BirimId,
                    UyaptaGorunmeDurumu = request.UyaptaGorunmeDurumu,
                    Durum = request.Durum,

                };

                _birimAgacRepository.Add(addedBirimAgac);
                await _birimAgacRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}