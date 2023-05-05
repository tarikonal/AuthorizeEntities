
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
using Business.Handlers.Projes.ValidationRules;

namespace Business.Handlers.Projes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProjeCommand : IRequest<IResult>
    {

        public long? UstProjeId { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public string UrlAdresi { get; set; }
        public string BakimUrlAdresi { get; set; }
        public long? AgId { get; set; }
        public long? TipId { get; set; }
        public string Logo { get; set; }
        public string Icon { get; set; }
        public string IconText1 { get; set; }
        public string IconText2 { get; set; }
        public string IconText3 { get; set; }
        public string Ico { get; set; }
        public string KullanimKlavuzu { get; set; }
        public bool? Durum { get; set; }


        public class CreateProjeCommandHandler : IRequestHandler<CreateProjeCommand, IResult>
        {
            private readonly IProjeRepository _projeRepository;
            private readonly IMediator _mediator;
            public CreateProjeCommandHandler(IProjeRepository projeRepository, IMediator mediator)
            {
                _projeRepository = projeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProjeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProjeCommand request, CancellationToken cancellationToken)
            {
                var isThereProjeRecord = _projeRepository.Query().Any(u => u.UstProjeId == request.UstProjeId);

                if (isThereProjeRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProje = new Proje
                {
                    UstProjeId = request.UstProjeId,
                    Adi = request.Adi,
                    Aciklama = request.Aciklama,
                    UrlAdresi = request.UrlAdresi,
                    BakimUrlAdresi = request.BakimUrlAdresi,
                    //AgId = request.AgId,
                    //TipId = request.TipId,
                    Logo = request.Logo,
                    Icon = request.Icon,
                    IconText1 = request.IconText1,
                    IconText2 = request.IconText2,
                    IconText3 = request.IconText3,
                    Ico = request.Ico,
                    KullanimKlavuzu = request.KullanimKlavuzu,
                    Durum = request.Durum,

                };

                _projeRepository.Add(addedProje);
                await _projeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}