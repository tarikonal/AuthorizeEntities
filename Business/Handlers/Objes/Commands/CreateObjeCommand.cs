
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
using Business.Handlers.Objes.ValidationRules;

namespace Business.Handlers.Objes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateObjeCommand : IRequest<IResult>
    {

        public string ObjeAdi { get; set; }
        public string Aciklama { get; set; }
        public bool? Durum { get; set; }
        public long? ProjeId { get; set; }


        public class CreateObjeCommandHandler : IRequestHandler<CreateObjeCommand, IResult>
        {
            private readonly IObjeRepository _objeRepository;
            private readonly IMediator _mediator;
            public CreateObjeCommandHandler(IObjeRepository objeRepository, IMediator mediator)
            {
                _objeRepository = objeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateObjeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateObjeCommand request, CancellationToken cancellationToken)
            {
                var isThereObjeRecord = _objeRepository.Query().Any(u => u.ObjeAdi == request.ObjeAdi);

                if (isThereObjeRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedObje = new Obje
                {
                    ObjeAdi = request.ObjeAdi,
                    Aciklama = request.Aciklama,
                    Durum = request.Durum,
                    ProjeId = request.ProjeId,

                };

                _objeRepository.Add(addedObje);
                await _objeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}