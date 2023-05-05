
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Yetkis.Queries
{
    public class GetYetkiQuery : IRequest<IDataResult<Yetki>>
    {
        public long Id { get; set; }

        public class GetYetkiQueryHandler : IRequestHandler<GetYetkiQuery, IDataResult<Yetki>>
        {
            private readonly IYetkiRepository _yetkiRepository;
            private readonly IMediator _mediator;

            public GetYetkiQueryHandler(IYetkiRepository yetkiRepository, IMediator mediator)
            {
                _yetkiRepository = yetkiRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Yetki>> Handle(GetYetkiQuery request, CancellationToken cancellationToken)
            {
                var yetki = await _yetkiRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Yetki>(yetki);
            }
        }
    }
}
