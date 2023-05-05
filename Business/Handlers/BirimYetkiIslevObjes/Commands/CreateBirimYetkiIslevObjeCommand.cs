
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
using Business.Handlers.BirimYetkiIslevObjes.ValidationRules;

namespace Business.Handlers.BirimYetkiIslevObjes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateBirimYetkiIslevObjeCommand : IRequest<IResult>
    {

        public long? BirimId { get; set; }
        public long? YetkiId { get; set; }
        public long? IslevId { get; set; }
        public long? ObjeId { get; set; }
        public bool? Durum { get; set; }


        public class CreateBirimYetkiIslevObjeCommandHandler : IRequestHandler<CreateBirimYetkiIslevObjeCommand, IResult>
        {
            private readonly IBirimYetkiIslevObjeRepository _birimYetkiIslevObjeRepository;
            private readonly IMediator _mediator;
            public CreateBirimYetkiIslevObjeCommandHandler(IBirimYetkiIslevObjeRepository birimYetkiIslevObjeRepository, IMediator mediator)
            {
                _birimYetkiIslevObjeRepository = birimYetkiIslevObjeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateBirimYetkiIslevObjeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateBirimYetkiIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var isThereBirimYetkiIslevObjeRecord = _birimYetkiIslevObjeRepository.Query().Any(u => u.BirimId == request.BirimId);

                if (isThereBirimYetkiIslevObjeRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedBirimYetkiIslevObje = new BirimYetkiIslevObje
                {
                    BirimId = request.BirimId,
                    YetkiId = request.YetkiId,
                    IslevId = request.IslevId,
                    ObjeId = request.ObjeId,
                    Durum = request.Durum,

                };

                _birimYetkiIslevObjeRepository.Add(addedBirimYetkiIslevObje);
                await _birimYetkiIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}