
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

namespace Business.Handlers.Yetkis.Queries
{

    public class GetYetkisQuery : IRequest<IDataResult<IEnumerable<Yetki>>>
    {
        public class GetYetkisQueryHandler : IRequestHandler<GetYetkisQuery, IDataResult<IEnumerable<Yetki>>>
        {
            private readonly IYetkiRepository _yetkiRepository;
            private readonly IMediator _mediator;

            public GetYetkisQueryHandler(IYetkiRepository yetkiRepository, IMediator mediator)
            {
                _yetkiRepository = yetkiRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Yetki>>> Handle(GetYetkisQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Yetki>>(await _yetkiRepository.GetListAsync());
            }
        }
    }
}