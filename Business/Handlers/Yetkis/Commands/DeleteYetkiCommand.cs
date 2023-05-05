
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Yetkis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteYetkiCommand : IRequest<IResult>
    {
        public long Id { get; set; }

        public class DeleteYetkiCommandHandler : IRequestHandler<DeleteYetkiCommand, IResult>
        {
            private readonly IYetkiRepository _yetkiRepository;
            private readonly IMediator _mediator;

            public DeleteYetkiCommandHandler(IYetkiRepository yetkiRepository, IMediator mediator)
            {
                _yetkiRepository = yetkiRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteYetkiCommand request, CancellationToken cancellationToken)
            {
                var yetkiToDelete = _yetkiRepository.Get(p => p.Id == request.Id);

                _yetkiRepository.Delete(yetkiToDelete);
                await _yetkiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

