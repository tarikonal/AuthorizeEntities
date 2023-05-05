
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.BirimYetkiIslevObjes.Queries
{
    public class GetBirimYetkiIslevObjeQuery : IRequest<IDataResult<BirimYetkiIslevObje>>
    {
        public long Id { get; set; }

        public class GetBirimYetkiIslevObjeQueryHandler : IRequestHandler<GetBirimYetkiIslevObjeQuery, IDataResult<BirimYetkiIslevObje>>
        {
            private readonly IBirimYetkiIslevObjeRepository _birimYetkiIslevObjeRepository;
            private readonly IMediator _mediator;

            public GetBirimYetkiIslevObjeQueryHandler(IBirimYetkiIslevObjeRepository birimYetkiIslevObjeRepository, IMediator mediator)
            {
                _birimYetkiIslevObjeRepository = birimYetkiIslevObjeRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<BirimYetkiIslevObje>> Handle(GetBirimYetkiIslevObjeQuery request, CancellationToken cancellationToken)
            {
                var birimYetkiIslevObje = await _birimYetkiIslevObjeRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<BirimYetkiIslevObje>(birimYetkiIslevObje);
            }
        }
    }
}
