using System;
using System.ComponentModel.DataAnnotations;

namespace dotNet_GMZ_backend.Models.DTOModels
{
    public class FoundNewsRecordDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Header { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime DateTime { get; set; }
    }
}