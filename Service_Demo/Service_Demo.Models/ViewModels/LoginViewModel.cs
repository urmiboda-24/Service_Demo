using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Required")]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,10}$", ErrorMessage = "Password must contain at least one uppercase, one lower case, one digit and length must be between 6 to 10")]
        public string Password { get; set; }
    }
}
