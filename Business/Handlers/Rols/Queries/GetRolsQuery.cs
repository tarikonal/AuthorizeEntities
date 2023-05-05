
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

namespace Business.Handlers.Rols.Queries
{

    public class GetRolsQuery : IRequest<IDataResult<IEnumerable<Rol>>>
    {
        public class GetRolsQueryHandler : IRequestHandler<GetRolsQuery, IDataResult<IEnumerable<Rol>>>
        {
            private readonly IRolRepository _rolRepository;
            private readonly IMediator _mediator;

            public GetRolsQueryHandler(IRolRepository rolRepository, IMediator mediator)
            {
                _rolRepository = rolRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Rol>>> Handle(GetRolsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Rol>>(await _rolRepository.GetListAsync());
            }
        }
    }
}