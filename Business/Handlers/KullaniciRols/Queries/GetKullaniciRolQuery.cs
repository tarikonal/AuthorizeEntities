
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.KullaniciRols.Queries
{
    public class GetKullaniciRolQuery : IRequest<IDataResult<KullaniciRol>>
    {
        public long Id { get; set; }

        public class GetKullaniciRolQueryHandler : IRequestHandler<GetKullaniciRolQuery, IDataResult<KullaniciRol>>
        {
            private readonly IKullaniciRolRepository _kullaniciRolRepository;
            private readonly IMediator _mediator;

            public GetKullaniciRolQueryHandler(IKullaniciRolRepository kullaniciRolRepository, IMediator mediator)
            {
                _kullaniciRolRepository = kullaniciRolRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<KullaniciRol>> Handle(GetKullaniciRolQuery request, CancellationToken cancellationToken)
            {
                var kullaniciRol = await _kullaniciRolRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<KullaniciRol>(kullaniciRol);
            }
        }
    }
}
