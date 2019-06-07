using System.ComponentModel.DataAnnotations;

namespace Architecture.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "A {0} deve ter {1} caracteres.")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas informadas não coincidem.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
