using Weblog.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weblog.Core.Enums;

namespace Weblog.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Name can't be blank.")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Email can't be blank.")]
        [EmailAddress(ErrorMessage = "Email Should be in a proper email address format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can't be blank.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password can't be blank.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password dont match")]
        public string ConfirmPassword { get; set; }

        public UserTypeOptions UserType { get; set; } = UserTypeOptions.User;
    }
}
