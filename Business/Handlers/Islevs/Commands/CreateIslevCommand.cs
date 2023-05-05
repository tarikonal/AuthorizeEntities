
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Islevs.ValidationRules;

namespace Business.Handlers.Islevs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateIslevCommand : IRequest<IResult>
    {

        public string IslevAdi { get; set; }
        public string Aciklama { get; set; }
        public bool? Durum { get; set; }
        public long? ProjeId { get; set; }


        public class CreateIslevCommandHandler : IRequestHandler<CreateIslevCommand, IResult>
        {
            private readonly IIslevRepository _islevRepository;
            private readonly IMediator _mediator;
            public CreateIslevCommandHandler(IIslevRepository islevRepository, IMediator mediator)
            {
                _islevRepository = islevRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateIslevValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateIslevCommand request, CancellationToken cancellationToken)
            {
                var isThereIslevRecord = _islevRepository.Query().Any(u => u.IslevAdi == request.IslevAdi);

                if (isThereIslevRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedIslev = new Islev
                {
                    IslevAdi = request.IslevAdi,
                    Aciklama = request.Aciklama,
                    Durum = request.Durum,
                    ProjeId = request.ProjeId,

                };

                _islevRepository.Add(addedIslev);
                await _islevRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}