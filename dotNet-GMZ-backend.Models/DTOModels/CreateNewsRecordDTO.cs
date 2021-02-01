using System;
using System.ComponentModel.DataAnnotations;

namespace dotNet_GMZ_backend.Models.ModelsDTO
{
    public class CreateNewsRecordDTO
    {
        [Required]
        public string Header { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}