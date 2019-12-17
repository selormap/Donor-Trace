using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DonorTraceAPI.Data;
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
        private readonly DataContext _db;
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
            //if model state is not valid
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            //check if user already exists
            User user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null) return BadRequest(ModelState); //user exist

            var now = DateTime.Now;
            //user does not exist, create new user.
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
                return Ok(); //return a success status message

            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<SuccessfulLoginResult>> Login(LoginDto login)
        {
            //check if email and password matches
            Microsoft.AspNetCore.Identity.SignInResult result = await
            _signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent: false, lockoutOnFailure: false);

            //no match found
            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            //match found
            User user = await _userManager.FindByNameAsync(login.Email);
            JwtSecurityToken token = await GenerateTokenAsync(user); //generate json web token
            var roles = await _userManager.GetRolesAsync(user); // get user roles
            
            string serializedToken = new
            JwtSecurityTokenHandler().WriteToken(token); //serialize the token
            //return status code with response object
            return Ok(new SuccessfulLoginResult()
            {
                Token = serializedToken,
                Id = user.Id,
               Role = roles.FirstOrDefault()
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

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            //check if model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // get user
            var user = await _userManager.FindByIdAsync(model.UserId);
            // change password
            IdentityResult result = await _userManager.ChangePasswordAsync(user,model.OldPassword, model.NewPassword);

            //password change did not succeed
            if (!result.Succeeded)
            {
                return BadRequest("Could not change password");
            }
            //password change succeeded
            return Ok();

        }

        [AllowAnonymous]
        [HttpPost("change-pass")]
        public async Task<IActionResult> ChangePass([FromBody] LoginDto model)
        {

            var user = await _userManager.FindByNameAsync("dtadmin");
            // var spaUser =  await ctx. ctx..FirstOrDefault(u => u.UserName == "edboatend@gmail.com");
            var newPassword = _userManager.PasswordHasher.HashPassword(user, "Password@1");
            user.PasswordHash = newPassword;
            var res = await _userManager.UpdateAsync(user);

            if (res.Succeeded) { return Ok(); }
            else { return BadRequest("Could not change user password"); }
        }
    }
}