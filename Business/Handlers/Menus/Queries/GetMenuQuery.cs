
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Menus.Queries
{
    public class GetMenuQuery : IRequest<IDataResult<Menu>>
    {
        public long Id { get; set; }

        public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, IDataResult<Menu>>
        {
            private readonly IMenuRepository _menuRepository;
            private readonly IMediator _mediator;

            public GetMenuQueryHandler(IMenuRepository menuRepository, IMediator mediator)
            {
                _menuRepository = menuRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Menu>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
            {
                var menu = await _menuRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Menu>(menu);
            }
        }
    }
}
