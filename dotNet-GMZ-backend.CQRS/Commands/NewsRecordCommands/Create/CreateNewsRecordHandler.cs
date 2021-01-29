using AutoMapper;
using dotNet_GMZ_backend.Core;
using dotNet_GMZ_backend.Models.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet_GMZ_backend.CQRS.Commands.NewsRecordCommands.Create
{
    public class CreateNewsRecordHandler : IRequestHandler<CreateNewsRecord, bool>

    {
        private readonly INewsRecordRepository _repository;
        private readonly ILogger<CreateNewsRecordHandler> _logger;
        private readonly IMapper _mapper;

        public CreateNewsRecordHandler(INewsRecordRepository repository, IMapper mapper, ILogger<CreateNewsRecordHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateNewsRecord request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _mapper.Map<NewsRecord>(request.NewsRecordDto);
                var response = await _repository.CreateAsync(result);
                await _repository.SaveAsync(cancellationToken);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(CreateNewsRecordHandler.Handle), e);
                return false;
            }
        }
    }
}