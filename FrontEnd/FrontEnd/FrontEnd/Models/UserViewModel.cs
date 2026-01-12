using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Code")]
        [Required(ErrorMessage = "Enter Code")]
        public string Code { get; set; } = null!;

        [Display(Name = "Fullname")]
        [Required(ErrorMessage = "Enter fullname")]
        public string FullName { get; set; } = null!;

        [Display(Name = "DOB")]
        [Required(ErrorMessage = "Enter DOB")]
        public DateOnly? DateOfBirth { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress(ErrorMessage = "Email is wrong format")]
        public string? Email { get; set; }

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Enter phone number")]
        [Phone(ErrorMessage = "Phone number is invalid")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Enter Address")]
        public string? Address { get; set; }
    }
}
