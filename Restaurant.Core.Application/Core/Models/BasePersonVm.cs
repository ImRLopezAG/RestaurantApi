
namespace Restaurant.Core.Application.Core.Models;

public class BasePersonVm : Base {
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string DNI { get; set; } = null!;
}
