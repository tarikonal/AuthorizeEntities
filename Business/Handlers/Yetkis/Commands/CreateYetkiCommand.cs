
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
using Business.Handlers.Yetkis.ValidationRules;

namespace Business.Handlers.Yetkis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateYetkiCommand : IRequest<IResult>
    {

        public string YetkiAdi { get; set; }
        public string Aciklama { get; set; }
        public bool? Durum { get; set; }
        public long? ProjeId { get; set; }


        public class CreateYetkiCommandHandler : IRequestHandler<CreateYetkiCommand, IResult>
        {
            private readonly IYetkiRepository _yetkiRepository;
            private readonly IMediator _mediator;
            public CreateYetkiCommandHandler(IYetkiRepository yetkiRepository, IMediator mediator)
            {
                _yetkiRepository = yetkiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateYetkiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateYetkiCommand request, CancellationToken cancellationToken)
            {
                var isThereYetkiRecord = _yetkiRepository.Query().Any(u => u.YetkiAdi == request.YetkiAdi);

                if (isThereYetkiRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedYetki = new Yetki
                {
                    YetkiAdi = request.YetkiAdi,
                    Aciklama = request.Aciklama,
                    Durum = request.Durum,
                    ProjeId = request.ProjeId,

                };

                _yetkiRepository.Add(addedYetki);
                await _yetkiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}