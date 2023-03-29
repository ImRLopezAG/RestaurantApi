using Restaurant.Core.Application.Dtos.Account;

namespace Restaurant.Core.Application.Contracts;

public interface IAccountService {
  Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
  Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin, bool isAdmin = false);
  Task SignOutAsync();
}
