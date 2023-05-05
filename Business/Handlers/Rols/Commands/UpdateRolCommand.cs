
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
using Business.Handlers.Rols.ValidationRules;


namespace Business.Handlers.Rols.Commands
{


    public class UpdateRolCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public long? ProjeId { get; set; }
        public long? RolTipiId { get; set; }
        public long? RolSeviyeId { get; set; }
        public string KeyValue { get; set; }
        public string RolAdi { get; set; }
        public string Aciklama { get; set; }
        public bool? VarsayilanMi { get; set; }
        public bool? Durum { get; set; }

        public class UpdateRolCommandHandler : IRequestHandler<UpdateRolCommand, IResult>
        {
            private readonly IRolRepository _rolRepository;
            private readonly IMediator _mediator;

            public UpdateRolCommandHandler(IRolRepository rolRepository, IMediator mediator)
            {
                _rolRepository = rolRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateRolValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateRolCommand request, CancellationToken cancellationToken)
            {
                var isThereRolRecord = await _rolRepository.GetAsync(u => u.Id == request.Id);


                isThereRolRecord.ProjeId = request.ProjeId;
                isThereRolRecord.RolTipiId = request.RolTipiId;
                isThereRolRecord.RolSeviyeId = request.RolSeviyeId;
                isThereRolRecord.KeyValue = request.KeyValue;
                isThereRolRecord.RolAdi = request.RolAdi;
                isThereRolRecord.Aciklama = request.Aciklama;
                isThereRolRecord.VarsayilanMi = request.VarsayilanMi;
                isThereRolRecord.Durum = request.Durum;


                _rolRepository.Update(isThereRolRecord);
                await _rolRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

