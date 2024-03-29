﻿using System.Text.Json.Serialization;

namespace Restaurant.Core.Application.Dtos.Account;
public class AuthenticationResponse {
  public string Id { get; set; } = null!;
  public string UserName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public List<string> Roles { get; set; } = null!;
  public bool IsVerified { get; set; }
  public bool HasError { get; set; }
  public string? Error { get; set; }
  public string JWToken { get; set; } = null!;

  [JsonIgnore]
  public string RefreshToken { get; set; } = null!;
}

