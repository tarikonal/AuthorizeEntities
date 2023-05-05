
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Islevs.Queries
{
    public class GetIslevQuery : IRequest<IDataResult<Islev>>
    {
        public long Id { get; set; }

        public class GetIslevQueryHandler : IRequestHandler<GetIslevQuery, IDataResult<Islev>>
        {
            private readonly IIslevRepository _islevRepository;
            private readonly IMediator _mediator;

            public GetIslevQueryHandler(IIslevRepository islevRepository, IMediator mediator)
            {
                _islevRepository = islevRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Islev>> Handle(GetIslevQuery request, CancellationToken cancellationToken)
            {
                var islev = await _islevRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Islev>(islev);
            }
        }
    }
}
