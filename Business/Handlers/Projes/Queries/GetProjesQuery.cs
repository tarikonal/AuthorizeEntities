﻿
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Projes.Queries
{

    public class GetProjesQuery : IRequest<IDataResult<IEnumerable<Proje>>>
    {
        public class GetProjesQueryHandler : IRequestHandler<GetProjesQuery, IDataResult<IEnumerable<Proje>>>
        {
            private readonly IProjeRepository _projeRepository;
            private readonly IMediator _mediator;

            public GetProjesQueryHandler(IProjeRepository projeRepository, IMediator mediator)
            {
                _projeRepository = projeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Proje>>> Handle(GetProjesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Proje>>(await _projeRepository.GetListAsync());
            }
        }
    }
}