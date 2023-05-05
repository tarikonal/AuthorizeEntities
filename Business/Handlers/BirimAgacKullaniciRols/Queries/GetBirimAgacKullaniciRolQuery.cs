
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.BirimAgacKullaniciRols.Queries
{
    public class GetBirimAgacKullaniciRolQuery : IRequest<IDataResult<BirimAgacKullaniciRol>>
    {
        public long Id { get; set; }

        public class GetBirimAgacKullaniciRolQueryHandler : IRequestHandler<GetBirimAgacKullaniciRolQuery, IDataResult<BirimAgacKullaniciRol>>
        {
            private readonly IBirimAgacKullaniciRolRepository _birimAgacKullaniciRolRepository;
            private readonly IMediator _mediator;

            public GetBirimAgacKullaniciRolQueryHandler(IBirimAgacKullaniciRolRepository birimAgacKullaniciRolRepository, IMediator mediator)
            {
                _birimAgacKullaniciRolRepository = birimAgacKullaniciRolRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<BirimAgacKullaniciRol>> Handle(GetBirimAgacKullaniciRolQuery request, CancellationToken cancellationToken)
            {
                var birimAgacKullaniciRol = await _birimAgacKullaniciRolRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<BirimAgacKullaniciRol>(birimAgacKullaniciRol);
            }
        }
    }
}
