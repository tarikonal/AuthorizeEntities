
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.KullaniciYetkiIslevEngels.Queries
{
    public class GetKullaniciYetkiIslevEngelQuery : IRequest<IDataResult<KullaniciYetkiIslevEngel>>
    {
        public long Id { get; set; }

        public class GetKullaniciYetkiIslevEngelQueryHandler : IRequestHandler<GetKullaniciYetkiIslevEngelQuery, IDataResult<KullaniciYetkiIslevEngel>>
        {
            private readonly IKullaniciYetkiIslevEngelRepository _kullaniciYetkiIslevEngelRepository;
            private readonly IMediator _mediator;

            public GetKullaniciYetkiIslevEngelQueryHandler(IKullaniciYetkiIslevEngelRepository kullaniciYetkiIslevEngelRepository, IMediator mediator)
            {
                _kullaniciYetkiIslevEngelRepository = kullaniciYetkiIslevEngelRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<KullaniciYetkiIslevEngel>> Handle(GetKullaniciYetkiIslevEngelQuery request, CancellationToken cancellationToken)
            {
                var kullaniciYetkiIslevEngel = await _kullaniciYetkiIslevEngelRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<KullaniciYetkiIslevEngel>(kullaniciYetkiIslevEngel);
            }
        }
    }
}
