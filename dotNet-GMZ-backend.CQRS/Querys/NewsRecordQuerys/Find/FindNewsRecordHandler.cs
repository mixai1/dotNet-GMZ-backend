using AutoMapper;
using dotNet_GMZ_backend.Core;
using dotNet_GMZ_backend.Models.DTOModels;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet_GMZ_backend.CQRS.Querys.NewsRecordQuerys.Find
{
    public class FindNewsRecordHandler : IRequestHandler<FindNewsRecord, FoundNewsRecordDTO>
    {
        private readonly INewsRecordRepository _repository;
        private readonly ILogger<FindNewsRecordHandler> _logger;
        private readonly IMapper _mapper;

        public FindNewsRecordHandler(INewsRecordRepository repository, ILogger<FindNewsRecordHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<FoundNewsRecordDTO> Handle(FindNewsRecord request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.FindById(request.Id);
                if (result != null)
                {
                    var response = _mapper.Map<FoundNewsRecordDTO>(result);
                    return response;
                }

                _logger.LogError(nameof(FindNewsRecordHandler.Handle));
                return new FoundNewsRecordDTO();
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(FindNewsRecordHandler.Handle), e);
                return new FoundNewsRecordDTO();
            }
        }
    }
}