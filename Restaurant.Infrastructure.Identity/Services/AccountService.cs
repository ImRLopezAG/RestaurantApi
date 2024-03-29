﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Dtos.Account;
using Restaurant.Core.Application.Enums;
using Restaurant.Infrastructure.Identity.Entities;
using Restaurant.Infrastructure.Identity.interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Restaurant.Infrastructure.Identity.Services;
public class AccountService : IAccountService {
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly SignInManager<ApplicationUser> _signInManager;

  private readonly IJwtService _jwtService;



  public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtService jwt) {
    _userManager = userManager;
    _signInManager = signInManager;
    _jwtService = jwt;
  }

  public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request) {
    AuthenticationResponse response = new();

    var user = await _userManager.FindByEmailAsync(request.Email);
    if (user == null) {
      response.HasError = true;
      response.Error = $"No Accounts registered with {request.Email}";
      return response;
    }

    var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
    if (!result.Succeeded) {
      response.HasError = true;
      response.Error = $"Invalid credentials for {request.Email}";
      return response;
    }
    if (!user.EmailConfirmed) {
      response.HasError = true;
      response.Error = $"Account no confirmed for {request.Email}";
      return response;
    }

    JwtSecurityToken token = await _jwtService.GenerateJwToken(user);

    response.Id = user.Id;
    response.Email = user.Email;
    response.UserName = user.UserName;
    response.JWToken = new JwtSecurityTokenHandler().WriteToken(token);
    response.RefreshToken = _jwtService.GenerateRefreshToken().Token;

    var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

    response.Roles = rolesList.ToList();
    response.IsVerified = user.EmailConfirmed;

    return response;
  }



  public async Task SignOutAsync() {
    await _signInManager.SignOutAsync();
  }

  public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin, bool isAdmin = false) {
    RegisterResponse response = new() {
      HasError = false
    };

    var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
    if (userWithSameUserName != null) {
      response.HasError = true;
      response.Error = $"username '{request.UserName}' is already taken.";
      return response;
    }

    var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
    if (userWithSameEmail != null) {
      response.HasError = true;
      response.Error = $"Email '{request.Email}' is already registered.";
      return response;
    }

    var user = new ApplicationUser {
      Email = request.Email,
      FirstName = request.FirstName,
      LastName = request.LastName,
      UserName = request.UserName,
      EmailConfirmed = true,
    };

    var result = await _userManager.CreateAsync(user, request.Password);
    if (result.Succeeded) {
      await _userManager.AddToRoleAsync(user, isAdmin ? Roles.Admin.ToString() : Roles.Waiter.ToString());
    } else {
      response.HasError = true;
      response.Error = $"An error occurred trying to register the user.";
      return response;
    }

    return response;
  }


  public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request) {
    ResetPasswordResponse response = new() {
      HasError = false
    };

    var user = await _userManager.FindByEmailAsync(request.Email);

    if (user == null) {
      response.HasError = true;
      response.Error = $"No Accounts registered with {request.Email}";
      return response;
    }

    request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
    var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

    if (!result.Succeeded) {
      response.HasError = true;
      response.Error = $"An error occurred while reset password";
      return response;
    }

    return response;
  }

}




