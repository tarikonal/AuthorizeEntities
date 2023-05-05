
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
using Business.Handlers.Projes.ValidationRules;


namespace Business.Handlers.Projes.Commands
{


    public class UpdateProjeCommand : IRequest<IResult>
    {
        public long Id { get; set; }
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

        public class UpdateProjeCommandHandler : IRequestHandler<UpdateProjeCommand, IResult>
        {
            private readonly IProjeRepository _projeRepository;
            private readonly IMediator _mediator;

            public UpdateProjeCommandHandler(IProjeRepository projeRepository, IMediator mediator)
            {
                _projeRepository = projeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProjeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProjeCommand request, CancellationToken cancellationToken)
            {
                var isThereProjeRecord = await _projeRepository.GetAsync(u => u.Id == request.Id);


                isThereProjeRecord.UstProjeId = request.UstProjeId;
                isThereProjeRecord.Adi = request.Adi;
                isThereProjeRecord.Aciklama = request.Aciklama;
                isThereProjeRecord.UrlAdresi = request.UrlAdresi;
                isThereProjeRecord.BakimUrlAdresi = request.BakimUrlAdresi;
                //isThereProjeRecord.AgId = request.AgId;
                //isThereProjeRecord.TipId = request.TipId;
                isThereProjeRecord.Logo = request.Logo;
                isThereProjeRecord.Icon = request.Icon;
                isThereProjeRecord.IconText1 = request.IconText1;
                isThereProjeRecord.IconText2 = request.IconText2;
                isThereProjeRecord.IconText3 = request.IconText3;
                isThereProjeRecord.Ico = request.Ico;
                isThereProjeRecord.KullanimKlavuzu = request.KullanimKlavuzu;
                isThereProjeRecord.Durum = request.Durum;


                _projeRepository.Update(isThereProjeRecord);
                await _projeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

