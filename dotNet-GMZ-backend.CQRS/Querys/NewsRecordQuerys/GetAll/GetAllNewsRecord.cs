using dotNet_GMZ_backend.Models.DTOModels;
using MediatR;
using System.Collections.Generic;

namespace dotNet_GMZ_backend.CQRS.Querys.NewsRecordQuerys.GetAll
{
    public class GetAllNewsRecord : IRequest<IEnumerable<FoundNewsRecordDTO>>
    {
    }
}