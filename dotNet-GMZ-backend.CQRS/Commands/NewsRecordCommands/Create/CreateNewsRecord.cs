using dotNet_GMZ_backend.Models.ModelsDTO;
using MediatR;

namespace dotNet_GMZ_backend.CQRS.Commands.NewsRecordCommands.Create
{
    public class CreateNewsRecord : IRequest<bool>
    {
        public NewsRecordDTO NewsRecordDto { get; }

        public CreateNewsRecord(NewsRecordDTO newsRecordDto)
        {
            NewsRecordDto = newsRecordDto;
        }
    }
}