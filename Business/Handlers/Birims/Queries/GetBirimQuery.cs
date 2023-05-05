
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Birims.Queries
{
    public class GetBirimQuery : IRequest<IDataResult<Birim>>
    {
        public long Id { get; set; }

        public class GetBirimQueryHandler : IRequestHandler<GetBirimQuery, IDataResult<Birim>>
        {
            private readonly IBirimRepository _birimRepository;
            private readonly IMediator _mediator;

            public GetBirimQueryHandler(IBirimRepository birimRepository, IMediator mediator)
            {
                _birimRepository = birimRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Birim>> Handle(GetBirimQuery request, CancellationToken cancellationToken)
            {
                var birim = await _birimRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Birim>(birim);
            }
        }
    }
}
