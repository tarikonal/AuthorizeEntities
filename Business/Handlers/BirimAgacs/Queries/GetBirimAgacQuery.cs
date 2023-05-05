
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.BirimAgacs.Queries
{
    public class GetBirimAgacQuery : IRequest<IDataResult<BirimAgac>>
    {
        public long Id { get; set; }

        public class GetBirimAgacQueryHandler : IRequestHandler<GetBirimAgacQuery, IDataResult<BirimAgac>>
        {
            private readonly IBirimAgacRepository _birimAgacRepository;
            private readonly IMediator _mediator;

            public GetBirimAgacQueryHandler(IBirimAgacRepository birimAgacRepository, IMediator mediator)
            {
                _birimAgacRepository = birimAgacRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<BirimAgac>> Handle(GetBirimAgacQuery request, CancellationToken cancellationToken)
            {
                var birimAgac = await _birimAgacRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<BirimAgac>(birimAgac);
            }
        }
    }
}
