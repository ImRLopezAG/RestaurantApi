namespace Restaurant.Core.Application.Dtos.Plate;

public class PlateSaveDto : BaseDto {
  public double Price { get; set; }
  public int Capacity { get; set; }

  public int CategoryId { get; set; }

  public ICollection<int> Ingredients { get; set; } = null!;

}
