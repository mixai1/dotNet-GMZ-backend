using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using dotNet_GMZ_backend.Core;

namespace dotNet_GMZ_backend.CQRS.Commands.NewsRecordCommands.Create
{
    public class CreateNewsRecordHandler : IRequestHandler<CreateNewsRecord,bool>

    {
        private readonly INewsRecordRepository _repository;

        public CreateNewsRecordHandler(INewsRecordRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CreateNewsRecord request, CancellationToken cancellationToken)
        {
            try
            {
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}