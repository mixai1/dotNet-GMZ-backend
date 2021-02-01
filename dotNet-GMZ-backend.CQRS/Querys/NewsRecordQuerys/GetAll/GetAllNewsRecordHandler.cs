using dotNet_GMZ_backend.Models.DTOModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet_GMZ_backend.CQRS.Querys.NewsRecordQuerys.GetAll
{
    public class GetAllNewsRecordHandler : IRequestHandler<GetAllNewsRecord, IEnumerable<FoundNewsRecordDTO>>
    {
        public Task<IEnumerable<FoundNewsRecordDTO>> Handle(GetAllNewsRecord request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}