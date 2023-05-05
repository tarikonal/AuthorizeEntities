
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
using Business.Handlers.Menus.ValidationRules;


namespace Business.Handlers.Menus.Commands
{


    public class UpdateMenuCommand : IRequest<IResult>
    {
        public long Id { get; set; }
        public long? UstMenuId { get; set; }
        public long? ProjeId { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public string Url { get; set; }
        public int Sira { get; set; }
        public string Icon { get; set; }
        public string IconText1 { get; set; }
        public string IconText2 { get; set; }
        public string IconText3 { get; set; }
        public bool? Durum { get; set; }

        public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, IResult>
        {
            private readonly IMenuRepository _menuRepository;
            private readonly IMediator _mediator;

            public UpdateMenuCommandHandler(IMenuRepository menuRepository, IMediator mediator)
            {
                _menuRepository = menuRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateMenuValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
            {
                var isThereMenuRecord = await _menuRepository.GetAsync(u => u.Id == request.Id);


                isThereMenuRecord.UstMenuId = request.UstMenuId;
                isThereMenuRecord.ProjeId = request.ProjeId;
                isThereMenuRecord.Adi = request.Adi;
                isThereMenuRecord.Aciklama = request.Aciklama;
                isThereMenuRecord.Url = request.Url;
                isThereMenuRecord.Sira = request.Sira;
                isThereMenuRecord.Icon = request.Icon;
                isThereMenuRecord.IconText1 = request.IconText1;
                isThereMenuRecord.IconText2 = request.IconText2;
                isThereMenuRecord.IconText3 = request.IconText3;
                isThereMenuRecord.Durum = request.Durum;


                _menuRepository.Update(isThereMenuRecord);
                await _menuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

