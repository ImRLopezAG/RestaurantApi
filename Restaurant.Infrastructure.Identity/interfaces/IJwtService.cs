using Restaurant.Core.Application.Dtos.Account;
using Restaurant.Infrastructure.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Restaurant.Infrastructure.Identity.interfaces;

public interface IJwtService {
  Task<JwtSecurityToken> GenerateJwToken(ApplicationUser user);
  RefreshToken GenerateRefreshToken();
}
