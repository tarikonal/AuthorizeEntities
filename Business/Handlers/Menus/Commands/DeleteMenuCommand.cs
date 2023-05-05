
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Menus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteMenuCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, IResult>
        {
            private readonly IMenuRepository _menuRepository;
            private readonly IMediator _mediator;

            public DeleteMenuCommandHandler(IMenuRepository menuRepository, IMediator mediator)
            {
                _menuRepository = menuRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
            {
                var menuToDelete = _menuRepository.Get(p => p.Id == request.Id);

                _menuRepository.Delete(menuToDelete);
                await _menuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

