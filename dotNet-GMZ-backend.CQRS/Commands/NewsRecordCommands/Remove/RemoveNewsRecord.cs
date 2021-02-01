using dotNet_GMZ_backend.Models.ModelsDTO;
using MediatR;

namespace dotNet_GMZ_backend.CQRS.Commands.NewsRecordCommands.Remove
{
    public class RemoveNewsRecord : IRequest<bool>
    {
        public RemoveNewsRecordDTO RemoveNewsRecordDto { get; }

        public RemoveNewsRecord(RemoveNewsRecordDTO removeNewsRecordDto)
        {
            RemoveNewsRecordDto = removeNewsRecordDto;
        }
    }
}