using System.Collections.Generic;

namespace dotNet_GMZ_backend.Models.DTOModels
{
    public class UserProfileDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> ListRoles { get; set; }
    }
}