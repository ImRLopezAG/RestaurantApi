using System.ComponentModel.DataAnnotations;

namespace Restaurant.Core.Application.ViewModels.User;

public class ForgotPasswordVm : ValidationVm {
  [Required(ErrorMessage = "The email is required")]
  [DataType(DataType.Text)]
  public string Email { get; set; } = null!;
}
