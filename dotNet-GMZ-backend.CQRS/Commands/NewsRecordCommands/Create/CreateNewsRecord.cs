using dotNet_GMZ_backend.Models.ModelsDTO;
using MediatR;

namespace dotNet_GMZ_backend.CQRS.Commands.NewsRecordCommands.Create
{
    public class CreateNewsRecord : IRequest<bool>
    {
        public CreateNewsRecordDTO NewsRecordDto { get; }

        public CreateNewsRecord(CreateNewsRecordDTO newsRecordDto)
        {
            NewsRecordDto = newsRecordDto;
        }
    }
}