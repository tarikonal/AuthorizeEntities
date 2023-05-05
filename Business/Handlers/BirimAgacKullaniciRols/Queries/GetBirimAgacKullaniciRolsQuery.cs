
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

namespace Business.Handlers.BirimAgacKullaniciRols.Queries
{

    public class GetBirimAgacKullaniciRolsQuery : IRequest<IDataResult<IEnumerable<BirimAgacKullaniciRol>>>
    {
        public class GetBirimAgacKullaniciRolsQueryHandler : IRequestHandler<GetBirimAgacKullaniciRolsQuery, IDataResult<IEnumerable<BirimAgacKullaniciRol>>>
        {
            private readonly IBirimAgacKullaniciRolRepository _birimAgacKullaniciRolRepository;
            private readonly IMediator _mediator;

            public GetBirimAgacKullaniciRolsQueryHandler(IBirimAgacKullaniciRolRepository birimAgacKullaniciRolRepository, IMediator mediator)
            {
                _birimAgacKullaniciRolRepository = birimAgacKullaniciRolRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<BirimAgacKullaniciRol>>> Handle(GetBirimAgacKullaniciRolsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<BirimAgacKullaniciRol>>(await _birimAgacKullaniciRolRepository.GetListAsync());
            }
        }
    }
}