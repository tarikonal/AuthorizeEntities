
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

namespace Business.Handlers.Objes.Queries
{

    public class GetObjesQuery : IRequest<IDataResult<IEnumerable<Obje>>>
    {
        public class GetObjesQueryHandler : IRequestHandler<GetObjesQuery, IDataResult<IEnumerable<Obje>>>
        {
            private readonly IObjeRepository _objeRepository;
            private readonly IMediator _mediator;

            public GetObjesQueryHandler(IObjeRepository objeRepository, IMediator mediator)
            {
                _objeRepository = objeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Obje>>> Handle(GetObjesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Obje>>(await _objeRepository.GetListAsync());
            }
        }
    }
}