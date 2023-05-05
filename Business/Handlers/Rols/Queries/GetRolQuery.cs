
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Rols.Queries
{
    public class GetRolQuery : IRequest<IDataResult<Rol>>
    {
        public long Id { get; set; }

        public class GetRolQueryHandler : IRequestHandler<GetRolQuery, IDataResult<Rol>>
        {
            private readonly IRolRepository _rolRepository;
            private readonly IMediator _mediator;

            public GetRolQueryHandler(IRolRepository rolRepository, IMediator mediator)
            {
                _rolRepository = rolRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Rol>> Handle(GetRolQuery request, CancellationToken cancellationToken)
            {
                var rol = await _rolRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Rol>(rol);
            }
        }
    }
}
