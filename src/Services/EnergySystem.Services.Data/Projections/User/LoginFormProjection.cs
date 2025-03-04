namespace EnergySystem.Services.Data.Projections.User
{
    using System.ComponentModel.DataAnnotations;

    public class LoginFormProjection
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
