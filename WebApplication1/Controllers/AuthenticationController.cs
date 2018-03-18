using Lab.Data;
using Lab.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly LabDbContext _dbContext;
        private readonly SignInManager<LabUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthenticationController(LabDbContext dbContext,
            SignInManager<LabUser> signInManager,
            IConfiguration config)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpGet("windowsAuth")]
        public async Task<IActionResult> WindowsAuthentication()
        {
            var response = Unauthorized();
            var windowsUser = WindowsIdentity.GetCurrent();

            if(windowsUser == null)
            {
                return response;
            }

            if(windowsUser.IsAuthenticated
                && !string.IsNullOrEmpty(windowsUser.Name))
            {
                var userName = windowsUser.Name;
                var domainAndUserName = windowsUser.Name.Split('\\');
                if (domainAndUserName.Length == 2)
                {
                    userName = domainAndUserName[1];
                }
                var labUser = await _signInManager.UserManager.FindByNameAsync(userName);
                if(labUser != null)
                {
                    var token = BuildToken(labUser);
                    return Ok(token);
                }
            }

            return response;
        }

        [HttpGet("loginAuth")]
        public async Task<IActionResult> CredentialAuthentication(LoginModel user)
        {
            var response = Unauthorized();

            if (ModelState.IsValid)
            {
                var labUser = await _signInManager.UserManager.FindByNameAsync(user.UserName);
                if (labUser != null)
                {
                    if (await _signInManager.UserManager.CheckPasswordAsync(labUser, user.Password))
                    {
                        var token = BuildToken(labUser);
                        return Ok(token);
                    }

                    return response;
                }

                return response;
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new LabUser { UserName = model.UserName };
                var result = await _signInManager.UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(result);
                }

                return BadRequest(result);
            }

            // If we got this far, something failed, redisplay form
            return  BadRequest(ModelState);
        }

        private string BuildToken(LabUser labUser)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, labUser.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, labUser.UserName),
                new Claim(JwtRegisteredClaimNames.UniqueName, labUser.UserName),
                new Claim("gender", "male")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.UtcNow.AddMinutes(5),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
