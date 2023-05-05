
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

namespace Business.Handlers.KullaniciMenuIslevObjes.Queries
{

    public class GetKullaniciMenuIslevObjesQuery : IRequest<IDataResult<IEnumerable<KullaniciMenuIslevObje>>>
    {
        public class GetKullaniciMenuIslevObjesQueryHandler : IRequestHandler<GetKullaniciMenuIslevObjesQuery, IDataResult<IEnumerable<KullaniciMenuIslevObje>>>
        {
            private readonly IKullaniciMenuIslevObjeRepository _kullaniciMenuIslevObjeRepository;
            private readonly IMediator _mediator;

            public GetKullaniciMenuIslevObjesQueryHandler(IKullaniciMenuIslevObjeRepository kullaniciMenuIslevObjeRepository, IMediator mediator)
            {
                _kullaniciMenuIslevObjeRepository = kullaniciMenuIslevObjeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<KullaniciMenuIslevObje>>> Handle(GetKullaniciMenuIslevObjesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<KullaniciMenuIslevObje>>(await _kullaniciMenuIslevObjeRepository.GetListAsync());
            }
        }
    }
}