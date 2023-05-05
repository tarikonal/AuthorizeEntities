
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.KullaniciMenuIslevObjes.Queries
{
    public class GetKullaniciMenuIslevObjeQuery : IRequest<IDataResult<KullaniciMenuIslevObje>>
    {
        public long Id { get; set; }

        public class GetKullaniciMenuIslevObjeQueryHandler : IRequestHandler<GetKullaniciMenuIslevObjeQuery, IDataResult<KullaniciMenuIslevObje>>
        {
            private readonly IKullaniciMenuIslevObjeRepository _kullaniciMenuIslevObjeRepository;
            private readonly IMediator _mediator;

            public GetKullaniciMenuIslevObjeQueryHandler(IKullaniciMenuIslevObjeRepository kullaniciMenuIslevObjeRepository, IMediator mediator)
            {
                _kullaniciMenuIslevObjeRepository = kullaniciMenuIslevObjeRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<KullaniciMenuIslevObje>> Handle(GetKullaniciMenuIslevObjeQuery request, CancellationToken cancellationToken)
            {
                var kullaniciMenuIslevObje = await _kullaniciMenuIslevObjeRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<KullaniciMenuIslevObje>(kullaniciMenuIslevObje);
            }
        }
    }
}
