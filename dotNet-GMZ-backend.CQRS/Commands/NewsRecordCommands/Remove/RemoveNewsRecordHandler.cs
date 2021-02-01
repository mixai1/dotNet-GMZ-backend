using dotNet_GMZ_backend.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet_GMZ_backend.CQRS.Commands.NewsRecordCommands.Remove
{
    public class RemoveNewsRecordHandler : IRequestHandler<RemoveNewsRecord, bool>
    {
        private readonly INewsRecordRepository _repository;
        private readonly ILogger<RemoveNewsRecordHandler> _logger;

        public RemoveNewsRecordHandler(INewsRecordRepository repository, ILogger<RemoveNewsRecordHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> Handle(RemoveNewsRecord request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.RemoveById(request.RemoveNewsRecordDto.Id);
                await _repository.SaveAsync(cancellationToken);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(RemoveNewsRecordHandler.Handle), e);
                return false;
            }
        }
    }
}