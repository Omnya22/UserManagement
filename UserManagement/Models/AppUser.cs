using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class AppUser : IdentityUser
    {
        [Required, MaxLength(100)]
        public string FName { get; set; }

        [Required, MaxLength(100)]
        public string LName { get; set; }

        public byte[] ProfilePic { get; set; }
    }
}
