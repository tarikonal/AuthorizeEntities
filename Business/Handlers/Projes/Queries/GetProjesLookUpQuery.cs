using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Projes.Queries
{
    public class GetProjesLookUpQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public class GetLanguagesLookUpQueryHandler : IRequestHandler<GetProjesLookUpQuery,
                IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IProjeRepository _projeRepository;
            private readonly IMediator _mediator;

            public GetLanguagesLookUpQueryHandler(IProjeRepository projeRepository, IMediator mediator)
            {
                _projeRepository = projeRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetProjesLookUpQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SelectionItem>>(
                    await _projeRepository.GetProjesLookUp());
            }
        }
    }
}