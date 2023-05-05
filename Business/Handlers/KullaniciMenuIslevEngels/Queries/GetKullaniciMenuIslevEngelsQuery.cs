
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

namespace Business.Handlers.KullaniciMenuIslevEngels.Queries
{

    public class GetKullaniciMenuIslevEngelsQuery : IRequest<IDataResult<IEnumerable<KullaniciMenuIslevEngel>>>
    {
        public class GetKullaniciMenuIslevEngelsQueryHandler : IRequestHandler<GetKullaniciMenuIslevEngelsQuery, IDataResult<IEnumerable<KullaniciMenuIslevEngel>>>
        {
            private readonly IKullaniciMenuIslevEngelRepository _kullaniciMenuIslevEngelRepository;
            private readonly IMediator _mediator;

            public GetKullaniciMenuIslevEngelsQueryHandler(IKullaniciMenuIslevEngelRepository kullaniciMenuIslevEngelRepository, IMediator mediator)
            {
                _kullaniciMenuIslevEngelRepository = kullaniciMenuIslevEngelRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<KullaniciMenuIslevEngel>>> Handle(GetKullaniciMenuIslevEngelsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<KullaniciMenuIslevEngel>>(await _kullaniciMenuIslevEngelRepository.GetListAsync());
            }
        }
    }
}