using System.ComponentModel.DataAnnotations;
using System;

namespace dotNet_GMZ_backend.Models.ModelsDTO
{
    public class RemoveNewsRecordDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}