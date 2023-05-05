
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.KullaniciMenuIslevEngels.Queries
{
    public class GetKullaniciMenuIslevEngelQuery : IRequest<IDataResult<KullaniciMenuIslevEngel>>
    {
        public long Id { get; set; }

        public class GetKullaniciMenuIslevEngelQueryHandler : IRequestHandler<GetKullaniciMenuIslevEngelQuery, IDataResult<KullaniciMenuIslevEngel>>
        {
            private readonly IKullaniciMenuIslevEngelRepository _kullaniciMenuIslevEngelRepository;
            private readonly IMediator _mediator;

            public GetKullaniciMenuIslevEngelQueryHandler(IKullaniciMenuIslevEngelRepository kullaniciMenuIslevEngelRepository, IMediator mediator)
            {
                _kullaniciMenuIslevEngelRepository = kullaniciMenuIslevEngelRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<KullaniciMenuIslevEngel>> Handle(GetKullaniciMenuIslevEngelQuery request, CancellationToken cancellationToken)
            {
                var kullaniciMenuIslevEngel = await _kullaniciMenuIslevEngelRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<KullaniciMenuIslevEngel>(kullaniciMenuIslevEngel);
            }
        }
    }
}
