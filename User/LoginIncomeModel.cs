using System.ComponentModel.DataAnnotations;

namespace ChatTest.User
{
    public class LoginIncomeModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
