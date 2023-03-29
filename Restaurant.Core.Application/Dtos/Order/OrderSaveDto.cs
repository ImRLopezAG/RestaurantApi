using Restaurant.Core.Application.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Core.Application.Dtos.Order;

public class OrderSaveDto : Base {
  [Required(ErrorMessage = "Table is required")]
  public int TableId { get; set; }
  public int StatusId { get; set; }

  [Required(ErrorMessage = "Plates are required")]
  public List<int> Plates { get; set; } = null!;
}
