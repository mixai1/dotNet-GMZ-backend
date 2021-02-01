using System;
using dotNet_GMZ_backend.Models.DTOModels;
using dotNet_GMZ_backend.Models.ModelsDTO;
using MediatR;

namespace dotNet_GMZ_backend.CQRS.Querys.NewsRecordQuerys.Find
{
    public class FindNewsRecord : IRequest<FoundNewsRecordDTO>
    {
        public Guid Id { get; }
        public FindNewsRecord(Guid id)
        {
            Id = id;
        }
    }
}