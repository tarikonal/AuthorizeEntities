
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

namespace Business.Handlers.KullaniciYetkiIslevEngels.Queries
{

    public class GetKullaniciYetkiIslevEngelsQuery : IRequest<IDataResult<IEnumerable<KullaniciYetkiIslevEngel>>>
    {
        public class GetKullaniciYetkiIslevEngelsQueryHandler : IRequestHandler<GetKullaniciYetkiIslevEngelsQuery, IDataResult<IEnumerable<KullaniciYetkiIslevEngel>>>
        {
            private readonly IKullaniciYetkiIslevEngelRepository _kullaniciYetkiIslevEngelRepository;
            private readonly IMediator _mediator;

            public GetKullaniciYetkiIslevEngelsQueryHandler(IKullaniciYetkiIslevEngelRepository kullaniciYetkiIslevEngelRepository, IMediator mediator)
            {
                _kullaniciYetkiIslevEngelRepository = kullaniciYetkiIslevEngelRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<KullaniciYetkiIslevEngel>>> Handle(GetKullaniciYetkiIslevEngelsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<KullaniciYetkiIslevEngel>>(await _kullaniciYetkiIslevEngelRepository.GetListAsync());
            }
        }
    }
}