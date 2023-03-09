using AutoMapper;
using Microsoft.AspNetCore.Http;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Dtos.Account;
using Restaurant.Core.Application.ViewModels.SaveVm;
using Restaurant.Core.Application.ViewModels.User;

namespace Restaurant.Core.Application.Services;

public class UserService : IUserService {
  private readonly IAccountService _accountService;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IMapper _mapper;

  public UserService(IAccountService accountService, IHttpContextAccessor httpContextAccessor,  IMapper mapper) {
    _accountService = accountService;
    _httpContextAccessor = httpContextAccessor;
    _mapper = mapper;
  }

  public async Task<AuthenticationResponse> LoginAsync(LoginVm vm) {
    AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
    AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
    return userResponse;
  }
  public async Task SignOutAsync() {
    await _accountService.SignOutAsync();
  }

  public async Task<RegisterResponse> RegisterAsync(SaveUserVm vm, string origin) {
    RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
    return await _accountService.RegisterBasicUserAsync(registerRequest, origin);
  }

}
