using System.Security.Claims;
using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly TokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager, TokenService tokenService)
    {
      _userManager = userManager;
      _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
    {
      var user = await _userManager.FindByEmailAsync(loginDTO.Email);
      if (user == null) return Unauthorized();

      var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

      if (result)
      {
        return new UserDTO
        {
          DisplayName = user.DisplayName,
          Image = null,
          Token = _tokenService.CreateToken(user),
          UserName = user.UserName
        };
      }

      return Unauthorized();
    }

    [AllowAnonymous]
    [Route("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
    {
      if (await _userManager.Users.AnyAsync(x => x.UserName == registerDTO.UserName))
      {
        return BadRequest("Username is already taken");
      }

      var user = new AppUser
      {
        DisplayName = registerDTO.DisplayName,
        Email = registerDTO.Email,
        UserName = registerDTO.UserName,
      };

      var result = await _userManager.CreateAsync(user, registerDTO.Password);

      if (result.Succeeded)
      {
        return CreateUserobject(user);
      }

      return BadRequest(result.Errors);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDTO>> GetCurrentUser()
    {
      var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
      return CreateUserobject(user);
    }

    private UserDTO CreateUserobject(AppUser user)
    {
      return new UserDTO
      {
        DisplayName = user.DisplayName,
        Image = null,
        Token = _tokenService.CreateToken(user),
        UserName = user.UserName
      };
    }
  }
}