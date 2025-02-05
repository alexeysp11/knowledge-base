using System.ComponentModel.DataAnnotations;

namespace KnowledgeBase.Examples.ManageTranslationsAspNetCore.ViewComponentTranslationMvc.ViewModels;

public class SignInViewModel
{
    [Key]
    public long UserId { get; set; }

    [Required(ErrorMessage = "Username is requried!")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is requried!")]
    public string Password { get; set; }
}