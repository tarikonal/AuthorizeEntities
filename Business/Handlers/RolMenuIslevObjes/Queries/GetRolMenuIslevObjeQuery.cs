
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.RolMenuIslevObjes.Queries
{
    public class GetRolMenuIslevObjeQuery : IRequest<IDataResult<RolMenuIslevObje>>
    {
        public long Id { get; set; }

        public class GetRolMenuIslevObjeQueryHandler : IRequestHandler<GetRolMenuIslevObjeQuery, IDataResult<RolMenuIslevObje>>
        {
            private readonly IRolMenuIslevObjeRepository _rolMenuIslevObjeRepository;
            private readonly IMediator _mediator;

            public GetRolMenuIslevObjeQueryHandler(IRolMenuIslevObjeRepository rolMenuIslevObjeRepository, IMediator mediator)
            {
                _rolMenuIslevObjeRepository = rolMenuIslevObjeRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<RolMenuIslevObje>> Handle(GetRolMenuIslevObjeQuery request, CancellationToken cancellationToken)
            {
                var rolMenuIslevObje = await _rolMenuIslevObjeRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<RolMenuIslevObje>(rolMenuIslevObje);
            }
        }
    }
}
