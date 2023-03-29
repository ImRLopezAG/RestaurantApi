using System.ComponentModel.DataAnnotations;

namespace Restaurant.Core.Application.Dtos.Plate;

public class PlateSaveDto : BaseDto {
  [Required(ErrorMessage = "Price is required")]
  public double Price { get; set; }
  [Required(ErrorMessage = "Capacity is required")]
  public int Capacity { get; set; }
  [Required(ErrorMessage = "Category is required")]
  public int CategoryId { get; set; }
  [Required(ErrorMessage = "Ingredients are required")]
  public ICollection<int> Ingredients { get; set; } = null!;

}
