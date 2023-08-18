using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
  }
}