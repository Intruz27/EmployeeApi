using FullStackApi.Models.DTO;
using FullStackApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FullStackApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthConroller : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepo _tokenRepository;
        
        public AuthConroller(UserManager<IdentityUser> userManager, ITokenRepo tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegistertDto registerRequestDto) {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if(identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                }
                

                if (identityResult.Succeeded) {
                    return Ok("User was registered! Please login.");
                }

            }
            return BadRequest("Something went wrong");

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult) {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null) {
                        var jwtToken = _tokenRepository.CreateJwtToken(user, roles.ToList());
                        var response = new LoginTokenDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }
                } 
            }
            return BadRequest("Username or Password is incorrect.");
        }

    }
}
