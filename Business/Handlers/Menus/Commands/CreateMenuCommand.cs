
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
using Business.Handlers.Menus.ValidationRules;

namespace Business.Handlers.Menus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateMenuCommand : IRequest<IResult>
    {

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


        public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, IResult>
        {
            private readonly IMenuRepository _menuRepository;
            private readonly IMediator _mediator;
            public CreateMenuCommandHandler(IMenuRepository menuRepository, IMediator mediator)
            {
                _menuRepository = menuRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateMenuValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
            {
                var isThereMenuRecord = _menuRepository.Query().Any(u => u.UstMenuId == request.UstMenuId);

                if (isThereMenuRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedMenu = new Menu
                {
                    UstMenuId = request.UstMenuId,
                    ProjeId = request.ProjeId,
                    Adi = request.Adi,
                    Aciklama = request.Aciklama,
                    Url = request.Url,
                    Sira = request.Sira,
                    Icon = request.Icon,
                    IconText1 = request.IconText1,
                    IconText2 = request.IconText2,
                    IconText3 = request.IconText3,
                    Durum = request.Durum,

                };

                _menuRepository.Add(addedMenu);
                await _menuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}