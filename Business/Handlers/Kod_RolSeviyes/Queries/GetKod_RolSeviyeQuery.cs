
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Kod_RolSeviyes.Queries
{
    public class GetKod_RolSeviyeQuery : IRequest<IDataResult<Kod_RolSeviye>>
    {
        public long Id { get; set; }

        public class GetKod_RolSeviyeQueryHandler : IRequestHandler<GetKod_RolSeviyeQuery, IDataResult<Kod_RolSeviye>>
        {
            private readonly IKod_RolSeviyeRepository _kod_RolSeviyeRepository;
            private readonly IMediator _mediator;

            public GetKod_RolSeviyeQueryHandler(IKod_RolSeviyeRepository kod_RolSeviyeRepository, IMediator mediator)
            {
                _kod_RolSeviyeRepository = kod_RolSeviyeRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Kod_RolSeviye>> Handle(GetKod_RolSeviyeQuery request, CancellationToken cancellationToken)
            {
                var kod_RolSeviye = await _kod_RolSeviyeRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Kod_RolSeviye>(kod_RolSeviye);
            }
        }
    }
}
