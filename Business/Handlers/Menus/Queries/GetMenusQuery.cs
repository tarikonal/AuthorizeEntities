
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Menus.Queries
{

    public class GetMenusQuery : IRequest<IDataResult<IEnumerable<Menu>>>
    {
        public class GetMenusQueryHandler : IRequestHandler<GetMenusQuery, IDataResult<IEnumerable<Menu>>>
        {
            private readonly IMenuRepository _menuRepository;
            private readonly IMediator _mediator;

            public GetMenusQueryHandler(IMenuRepository menuRepository, IMediator mediator)
            {
                _menuRepository = menuRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Menu>>> Handle(GetMenusQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Menu>>(await _menuRepository.GetListAsync());
            }
        }
    }
}