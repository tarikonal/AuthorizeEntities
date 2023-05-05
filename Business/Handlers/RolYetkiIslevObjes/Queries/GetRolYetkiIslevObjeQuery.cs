
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.RolYetkiIslevObjes.Queries
{
    public class GetRolYetkiIslevObjeQuery : IRequest<IDataResult<RolYetkiIslevObje>>
    {
        public long Id { get; set; }

        public class GetRolYetkiIslevObjeQueryHandler : IRequestHandler<GetRolYetkiIslevObjeQuery, IDataResult<RolYetkiIslevObje>>
        {
            private readonly IRolYetkiIslevObjeRepository _rolYetkiIslevObjeRepository;
            private readonly IMediator _mediator;

            public GetRolYetkiIslevObjeQueryHandler(IRolYetkiIslevObjeRepository rolYetkiIslevObjeRepository, IMediator mediator)
            {
                _rolYetkiIslevObjeRepository = rolYetkiIslevObjeRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<RolYetkiIslevObje>> Handle(GetRolYetkiIslevObjeQuery request, CancellationToken cancellationToken)
            {
                var rolYetkiIslevObje = await _rolYetkiIslevObjeRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<RolYetkiIslevObje>(rolYetkiIslevObje);
            }
        }
    }
}
