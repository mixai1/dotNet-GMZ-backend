using AutoMapper;
using dotNet_GMZ_backend.Core;
using dotNet_GMZ_backend.Models.DTOModels;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet_GMZ_backend.CQRS.Querys.NewsRecordQuerys.GetAll
{
    public class GetAllNewsRecordHandler : IRequestHandler<GetAllNewsRecord, IEnumerable<FoundNewsRecordDTO>>
    {
        private readonly INewsRecordRepository _repository;
        private readonly ILogger<GetAllNewsRecordHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllNewsRecordHandler(INewsRecordRepository repository, ILogger<GetAllNewsRecordHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FoundNewsRecordDTO>> Handle(GetAllNewsRecord request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetAllAsync();
                if (result != null)
                {
                    var response = _mapper.Map<IEnumerable<FoundNewsRecordDTO>>(result);
                    return response;
                }
                return new List<FoundNewsRecordDTO>();
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(GetAllNewsRecordHandler.Handle), e);
                return new List<FoundNewsRecordDTO>();
            }
        }
    }
}