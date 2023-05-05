
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

namespace Business.Handlers.BirimYetkiIslevObjes.Queries
{

    public class GetBirimYetkiIslevObjesQuery : IRequest<IDataResult<IEnumerable<BirimYetkiIslevObje>>>
    {
        public class GetBirimYetkiIslevObjesQueryHandler : IRequestHandler<GetBirimYetkiIslevObjesQuery, IDataResult<IEnumerable<BirimYetkiIslevObje>>>
        {
            private readonly IBirimYetkiIslevObjeRepository _birimYetkiIslevObjeRepository;
            private readonly IMediator _mediator;

            public GetBirimYetkiIslevObjesQueryHandler(IBirimYetkiIslevObjeRepository birimYetkiIslevObjeRepository, IMediator mediator)
            {
                _birimYetkiIslevObjeRepository = birimYetkiIslevObjeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<BirimYetkiIslevObje>>> Handle(GetBirimYetkiIslevObjesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<BirimYetkiIslevObje>>(await _birimYetkiIslevObjeRepository.GetListAsync());
            }
        }
    }
}