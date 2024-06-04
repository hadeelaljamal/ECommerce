using System.ComponentModel.DataAnnotations;

namespace ECommerce.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }


        [Required(ErrorMessage = "This Field is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Compare(nameof(Password), ErrorMessage = "Not Identical!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
