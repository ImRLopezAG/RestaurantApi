using Restaurant.Core.Application.Dtos;

namespace Restaurant.Core.Application.Contracts;

public interface IEmailService {
  Task SendEmail(EmailRequest request);
}
