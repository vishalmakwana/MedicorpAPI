using Medicorp.Core;
using Medicorp.Core.Entity;
using MedicorpWeb.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedicorpWeb.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(MemberRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var member = new ApplicationUser { UserName = model.Email, PhoneNumber = model.MobileNo, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName , IsActive = model.IsActive };
                var result = await _userManager.CreateAsync(member, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(member, false);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        return BadRequest(error.Description);
                    }
                }
                return Ok();
            }
            return BadRequest(ModelState);
        }


        private const string InvalidLoginDetails = "Invalid login details.";
        [AllowAnonymous]
        [HttpPost("Authentication")]
        public async Task<IActionResult> Authentication([FromBody] MemberCredentialModel userCredential)
        {
            ApiResponse<AuthResponseModel> response = new ApiResponse<AuthResponseModel>() { Success = true };
            ApplicationUser user = await _userManager.FindByNameAsync(userCredential.Email);
            if (user == null)
            {
                return BadRequest(InvalidLoginDetails);
            }
            //If valid user name then check password and if password is valid then processed for next.
            bool isValidPassword = await _userManager.CheckPasswordAsync(user, userCredential.Password);
            if (!isValidPassword)
            {
                return Unauthorized(InvalidLoginDetails);
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(userCredential.Email, userCredential.Password, false, true);
            if (result.IsLockedOut)
            {
                return BadRequest("This account has been locked out, please wait for 5 minute.");
            }
            if (result.Succeeded)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                     new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                #region CREATE TOKEN
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtTokenInfo:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(1));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtTokenInfo:Issuer"],
                    audience: _configuration["JwtTokenInfo:Audience"],
                    claims: claims,
                    expires: expires,
                    signingCredentials: creds
                );
                response.Result = new AuthResponseModel()
                {
                    IsActive = user.IsActive,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MobileNo = user.PhoneNumber,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    TokenExpires = expires
                };
                return Ok(response);
                #endregion
            }
            else
            {
                response.ConstructErrorResponse("AccountController Authentication","");
            }
            return BadRequest(response);
        }
    }
}