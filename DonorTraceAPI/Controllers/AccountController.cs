using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DonorTraceAPI.Dto;
using DonorTraceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DonorTraceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            User user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null) return BadRequest(ModelState);

            var now = DateTime.Now;

            user = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email,
                Email = model.Email,
                CreatedDate = now,
                LastModifiedDate = now

            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)

                return Ok();

            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<SuccessfulLoginResult>> Login(LoginDto login)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await
            _signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            User user = await _userManager.FindByEmailAsync(login.Email);
            JwtSecurityToken token = await GenerateTokenAsync(user);
            //defined
            string serializedToken = new
            JwtSecurityTokenHandler().WriteToken(token); //serialize the token
            return Ok(new SuccessfulLoginResult()
            {
                Token = serializedToken,
                Id = user.Id
            });
        }

        private async Task<JwtSecurityToken> GenerateTokenAsync(User user)
        {
            var claims = new List<Claim>()
                {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName),
            };

            // Loading the user Claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var expirationDays = _config.GetValue<int>("JWTConfiguration:TokenExpirationDays");
            var siginingKey =
            Encoding.UTF8.GetBytes(_config.GetValue<string>("JWTConfiguration:SigningKey"));

            var token = new JwtSecurityToken
            (
                //issuer: _config.GetValue<string>("JWTConfiguration:Issuer"),
                //audience: _config.GetValue<string>("JWTConfiguration:Audience"),
                claims: claims,
                expires:
                DateTime.UtcNow.Add(TimeSpan.FromDays(expirationDays)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new
                SymmetricSecurityKey(siginingKey),SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}