using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskITI.DT0;
using TaskITI.Models;

namespace TaskITI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    FirstName = registerDTO.FirstName,
                    LastName = registerDTO.LastName,
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email
                };

                IdentityResult result = await userManager.CreateAsync(applicationUser, registerDTO.Password);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return BadRequest(ModelState);
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (ModelState.IsValid) 
            {

                ApplicationUser? user = await userManager.FindByEmailAsync(loginDTO.Email);

                if (user != null)
                {
                    if (await userManager.CheckPasswordAsync(user, loginDTO.Password))
                    {
                        var claims = new List<Claim>();

                        //claims.Add(new Claim("tokenNo", "75"));
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var roles=await userManager.GetRolesAsync(user);
                        foreach (var item in roles) 
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item.ToString()));
                        }


                        //SigningCradiential

                        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                        var SigningCradiential = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);


                        var token = new JwtSecurityToken(
                            claims: claims,
                            issuer: configuration["JWT:Issuer"],
                            audience: configuration["JWT:Audience"],
                            expires: DateTime.Now.AddMonths(1),
                            signingCredentials: SigningCradiential
                            );
                        var _token = new
                        {
                            token=new JwtSecurityTokenHandler().WriteToken(token),
                            expiration=token.ValidTo
                        };
                        return Ok(_token);
                       



                    }
                    else 
                    {
                        return Unauthorized();
                    }

                }
                else 
                {
                    ModelState.AddModelError("", "Email not Found");
                }
            }
            return BadRequest(ModelState);
        }

    }
}
