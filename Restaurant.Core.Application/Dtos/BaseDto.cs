using Restaurant.Core.Application.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Core.Application.Dtos;

public class BaseDto : Base {
  [Required(ErrorMessage = "Name is required")]
  public string Name { get; set; } = null!;
}
