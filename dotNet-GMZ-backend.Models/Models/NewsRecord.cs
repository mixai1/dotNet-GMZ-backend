using System;
using System.ComponentModel.DataAnnotations;
using dotNet_GMZ_backend.Models.IdentityModels;

namespace dotNet_GMZ_backend.Models.Models
{
    public class NewsRecord : Entity
    {
        [Required]
        public string Header { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime DateTime { get; set; }
        public UserApp UserApp { get; set; }
    }
}