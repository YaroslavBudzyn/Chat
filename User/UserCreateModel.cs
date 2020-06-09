using System;
using System.ComponentModel.DataAnnotations;

namespace ChatTest.User
{
    public class UserCreateModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Range(1, 4)]
        public int RoleId { get; set; }
    }
}
