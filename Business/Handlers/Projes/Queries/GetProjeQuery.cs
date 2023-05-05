
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Projes.Queries
{
    public class GetProjeQuery : IRequest<IDataResult<Proje>>
    {
        public long Id { get; set; }

        public class GetProjeQueryHandler : IRequestHandler<GetProjeQuery, IDataResult<Proje>>
        {
            private readonly IProjeRepository _projeRepository;
            private readonly IMediator _mediator;

            public GetProjeQueryHandler(IProjeRepository projeRepository, IMediator mediator)
            {
                _projeRepository = projeRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Proje>> Handle(GetProjeQuery request, CancellationToken cancellationToken)
            {
                var proje = await _projeRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Proje>(proje);
            }
        }
    }
}
