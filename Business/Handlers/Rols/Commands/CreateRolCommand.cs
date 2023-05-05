
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
using Business.Handlers.Rols.ValidationRules;

namespace Business.Handlers.Rols.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateRolCommand : IRequest<IResult>
    {

        public long? ProjeId { get; set; }
        public long? RolTipiId { get; set; }
        public long? RolSeviyeId { get; set; }
        public string KeyValue { get; set; }
        public string RolAdi { get; set; }
        public string Aciklama { get; set; }
        public bool? VarsayilanMi { get; set; }
        public bool? Durum { get; set; }


        public class CreateRolCommandHandler : IRequestHandler<CreateRolCommand, IResult>
        {
            private readonly IRolRepository _rolRepository;
            private readonly IMediator _mediator;
            public CreateRolCommandHandler(IRolRepository rolRepository, IMediator mediator)
            {
                _rolRepository = rolRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateRolValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateRolCommand request, CancellationToken cancellationToken)
            {
                var isThereRolRecord = _rolRepository.Query().Any(u => u.ProjeId == request.ProjeId);

                if (isThereRolRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedRol = new Rol
                {
                    ProjeId = request.ProjeId,
                    RolTipiId = request.RolTipiId,
                    RolSeviyeId = request.RolSeviyeId,
                    KeyValue = request.KeyValue,
                    RolAdi = request.RolAdi,
                    Aciklama = request.Aciklama,
                    VarsayilanMi = request.VarsayilanMi,
                    Durum = request.Durum,

                };

                _rolRepository.Add(addedRol);
                await _rolRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}