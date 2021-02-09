using dotNet_GMZ_backend.Models.IdentityModels;
using System.ComponentModel.DataAnnotations;
using System;

namespace dotNet_GMZ_backend.Models.Models
{
    public class NewsRecord : Entity
    {
        [Required]
        public string Header { get; set; }
        [Required]
        public string SubTitles { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime DateTime { get; set; }
    }
}