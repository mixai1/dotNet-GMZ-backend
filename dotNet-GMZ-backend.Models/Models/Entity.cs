using System.ComponentModel.DataAnnotations;
using System;

namespace dotNet_GMZ_backend.Models.Models
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}