
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

namespace Business.Handlers.RolYetkiIslevObjes.Queries
{

    public class GetRolYetkiIslevObjesQuery : IRequest<IDataResult<IEnumerable<RolYetkiIslevObje>>>
    {
        public class GetRolYetkiIslevObjesQueryHandler : IRequestHandler<GetRolYetkiIslevObjesQuery, IDataResult<IEnumerable<RolYetkiIslevObje>>>
        {
            private readonly IRolYetkiIslevObjeRepository _rolYetkiIslevObjeRepository;
            private readonly IMediator _mediator;

            public GetRolYetkiIslevObjesQueryHandler(IRolYetkiIslevObjeRepository rolYetkiIslevObjeRepository, IMediator mediator)
            {
                _rolYetkiIslevObjeRepository = rolYetkiIslevObjeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<RolYetkiIslevObje>>> Handle(GetRolYetkiIslevObjesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<RolYetkiIslevObje>>(await _rolYetkiIslevObjeRepository.GetListAsync());
            }
        }
    }
}