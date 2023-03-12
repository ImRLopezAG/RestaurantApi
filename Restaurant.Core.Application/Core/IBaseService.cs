namespace Restaurant.Core.Application.Core;

public interface IBaseService {
  Task<ServiceResult> GetAll();
}
