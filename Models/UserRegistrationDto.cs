using System.ComponentModel.DataAnnotations;

namespace UserRegistrationApi.Models
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; } = "";

        [Required(ErrorMessage = "Industry is required")]
        public int IndustryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; } = "";

        public string Email { get; set; } = "";

        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms of service")]
        public bool TermsAccepted { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the privacy policy")]
        public bool PrivacyPolicyAccepted { get; set; }
    }
}
