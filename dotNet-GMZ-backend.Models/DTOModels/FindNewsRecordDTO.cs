using System;
using System.ComponentModel.DataAnnotations;

namespace dotNet_GMZ_backend.Models.ModelsDTO
{
    public class FindNewsRecordDTO
    {
        [Required]
        public Guid Id { get; set; }
       
    }
}