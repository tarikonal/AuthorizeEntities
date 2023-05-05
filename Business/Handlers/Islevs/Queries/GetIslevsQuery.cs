
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

namespace Business.Handlers.Islevs.Queries
{

    public class GetIslevsQuery : IRequest<IDataResult<IEnumerable<Islev>>>
    {
        public class GetIslevsQueryHandler : IRequestHandler<GetIslevsQuery, IDataResult<IEnumerable<Islev>>>
        {
            private readonly IIslevRepository _islevRepository;
            private readonly IMediator _mediator;

            public GetIslevsQueryHandler(IIslevRepository islevRepository, IMediator mediator)
            {
                _islevRepository = islevRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Islev>>> Handle(GetIslevsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Islev>>(await _islevRepository.GetListAsync());
            }
        }
    }
}