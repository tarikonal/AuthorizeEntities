
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
using Business.Handlers.BirimYetkiIslevObjes.ValidationRules;


namespace Business.Handlers.BirimYetkiIslevObjes.Commands
{


    public class UpdateBirimYetkiIslevObjeCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public long? BirimId { get; set; }
        public long? YetkiId { get; set; }
        public long? IslevId { get; set; }
        public long? ObjeId { get; set; }
        public bool? Durum { get; set; }

        public class UpdateBirimYetkiIslevObjeCommandHandler : IRequestHandler<UpdateBirimYetkiIslevObjeCommand, IResult>
        {
            private readonly IBirimYetkiIslevObjeRepository _birimYetkiIslevObjeRepository;
            private readonly IMediator _mediator;

            public UpdateBirimYetkiIslevObjeCommandHandler(IBirimYetkiIslevObjeRepository birimYetkiIslevObjeRepository, IMediator mediator)
            {
                _birimYetkiIslevObjeRepository = birimYetkiIslevObjeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateBirimYetkiIslevObjeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateBirimYetkiIslevObjeCommand request, CancellationToken cancellationToken)
            {
                var isThereBirimYetkiIslevObjeRecord = await _birimYetkiIslevObjeRepository.GetAsync(u => u.Id == request.Id);


                isThereBirimYetkiIslevObjeRecord.BirimId = request.BirimId;
                isThereBirimYetkiIslevObjeRecord.YetkiId = request.YetkiId;
                isThereBirimYetkiIslevObjeRecord.IslevId = request.IslevId;
                isThereBirimYetkiIslevObjeRecord.ObjeId = request.ObjeId;
                isThereBirimYetkiIslevObjeRecord.Durum = request.Durum;


                _birimYetkiIslevObjeRepository.Update(isThereBirimYetkiIslevObjeRecord);
                await _birimYetkiIslevObjeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

