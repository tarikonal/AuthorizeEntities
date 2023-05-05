
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

namespace Business.Handlers.Kod_RolSeviyes.Queries
{

    public class GetKod_RolSeviyesQuery : IRequest<IDataResult<IEnumerable<Kod_RolSeviye>>>
    {
        public class GetKod_RolSeviyesQueryHandler : IRequestHandler<GetKod_RolSeviyesQuery, IDataResult<IEnumerable<Kod_RolSeviye>>>
        {
            private readonly IKod_RolSeviyeRepository _kod_RolSeviyeRepository;
            private readonly IMediator _mediator;

            public GetKod_RolSeviyesQueryHandler(IKod_RolSeviyeRepository kod_RolSeviyeRepository, IMediator mediator)
            {
                _kod_RolSeviyeRepository = kod_RolSeviyeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Kod_RolSeviye>>> Handle(GetKod_RolSeviyesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Kod_RolSeviye>>(await _kod_RolSeviyeRepository.GetListAsync());
            }
        }
    }
}