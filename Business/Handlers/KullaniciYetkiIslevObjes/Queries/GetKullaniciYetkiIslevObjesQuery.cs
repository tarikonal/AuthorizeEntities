
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

namespace Business.Handlers.KullaniciYetkiIslevObjes.Queries
{

    public class GetKullaniciYetkiIslevObjesQuery : IRequest<IDataResult<IEnumerable<KullaniciYetkiIslevObje>>>
    {
        public class GetKullaniciYetkiIslevObjesQueryHandler : IRequestHandler<GetKullaniciYetkiIslevObjesQuery, IDataResult<IEnumerable<KullaniciYetkiIslevObje>>>
        {
            private readonly IKullaniciYetkiIslevObjeRepository _kullaniciYetkiIslevObjeRepository;
            private readonly IMediator _mediator;

            public GetKullaniciYetkiIslevObjesQueryHandler(IKullaniciYetkiIslevObjeRepository kullaniciYetkiIslevObjeRepository, IMediator mediator)
            {
                _kullaniciYetkiIslevObjeRepository = kullaniciYetkiIslevObjeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<KullaniciYetkiIslevObje>>> Handle(GetKullaniciYetkiIslevObjesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<KullaniciYetkiIslevObje>>(await _kullaniciYetkiIslevObjeRepository.GetListAsync());
            }
        }
    }
}