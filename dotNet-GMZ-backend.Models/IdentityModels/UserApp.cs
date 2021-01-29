using System.Collections.Generic;
using dotNet_GMZ_backend.Models.Models;
using Microsoft.AspNetCore.Identity;

namespace dotNet_GMZ_backend.Models.IdentityModels
{
    public class UserApp : IdentityUser
    {
        public List<NewsRecord> NewsRecords { get; set; }
    }
}