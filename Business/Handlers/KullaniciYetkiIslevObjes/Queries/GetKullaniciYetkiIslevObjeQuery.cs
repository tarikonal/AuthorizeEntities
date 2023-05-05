
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.KullaniciYetkiIslevObjes.Queries
{
    public class GetKullaniciYetkiIslevObjeQuery : IRequest<IDataResult<KullaniciYetkiIslevObje>>
    {
        public long Id { get; set; }

        public class GetKullaniciYetkiIslevObjeQueryHandler : IRequestHandler<GetKullaniciYetkiIslevObjeQuery, IDataResult<KullaniciYetkiIslevObje>>
        {
            private readonly IKullaniciYetkiIslevObjeRepository _kullaniciYetkiIslevObjeRepository;
            private readonly IMediator _mediator;

            public GetKullaniciYetkiIslevObjeQueryHandler(IKullaniciYetkiIslevObjeRepository kullaniciYetkiIslevObjeRepository, IMediator mediator)
            {
                _kullaniciYetkiIslevObjeRepository = kullaniciYetkiIslevObjeRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<KullaniciYetkiIslevObje>> Handle(GetKullaniciYetkiIslevObjeQuery request, CancellationToken cancellationToken)
            {
                var kullaniciYetkiIslevObje = await _kullaniciYetkiIslevObjeRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<KullaniciYetkiIslevObje>(kullaniciYetkiIslevObje);
            }
        }
    }
}
