
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

namespace Business.Handlers.RolMenuIslevObjes.Queries
{

    public class GetRolMenuIslevObjesQuery : IRequest<IDataResult<IEnumerable<RolMenuIslevObje>>>
    {
        public class GetRolMenuIslevObjesQueryHandler : IRequestHandler<GetRolMenuIslevObjesQuery, IDataResult<IEnumerable<RolMenuIslevObje>>>
        {
            private readonly IRolMenuIslevObjeRepository _rolMenuIslevObjeRepository;
            private readonly IMediator _mediator;

            public GetRolMenuIslevObjesQueryHandler(IRolMenuIslevObjeRepository rolMenuIslevObjeRepository, IMediator mediator)
            {
                _rolMenuIslevObjeRepository = rolMenuIslevObjeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<RolMenuIslevObje>>> Handle(GetRolMenuIslevObjesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<RolMenuIslevObje>>(await _rolMenuIslevObjeRepository.GetListAsync());
            }
        }
    }
}