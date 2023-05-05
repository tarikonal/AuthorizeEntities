
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

namespace Business.Handlers.KullaniciRols.Queries
{

    public class GetKullaniciRolsQuery : IRequest<IDataResult<IEnumerable<KullaniciRol>>>
    {
        public class GetKullaniciRolsQueryHandler : IRequestHandler<GetKullaniciRolsQuery, IDataResult<IEnumerable<KullaniciRol>>>
        {
            private readonly IKullaniciRolRepository _kullaniciRolRepository;
            private readonly IMediator _mediator;

            public GetKullaniciRolsQueryHandler(IKullaniciRolRepository kullaniciRolRepository, IMediator mediator)
            {
                _kullaniciRolRepository = kullaniciRolRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<KullaniciRol>>> Handle(GetKullaniciRolsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<KullaniciRol>>(await _kullaniciRolRepository.GetListAsync());
            }
        }
    }
}