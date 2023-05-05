
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Objes.Queries
{
    public class GetObjeQuery : IRequest<IDataResult<Obje>>
    {
        public long Id { get; set; }

        public class GetObjeQueryHandler : IRequestHandler<GetObjeQuery, IDataResult<Obje>>
        {
            private readonly IObjeRepository _objeRepository;
            private readonly IMediator _mediator;

            public GetObjeQueryHandler(IObjeRepository objeRepository, IMediator mediator)
            {
                _objeRepository = objeRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Obje>> Handle(GetObjeQuery request, CancellationToken cancellationToken)
            {
                var obje = await _objeRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Obje>(obje);
            }
        }
    }
}
