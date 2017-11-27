using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackingSystem.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using TrackingSystem.Services;
using Microsoft.Extensions.Logging;
using TrackingSystemBLayer.Authentication;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;

namespace TrackingSystem.Api
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly UserManager<IUser<int>> userManager;
        private readonly SignInManager<IUser<int>> signInManager;
        private readonly IEmailSender emailSender;
        private readonly IConfiguration config;
        private readonly ILogger logger;

        public TokenController(UserManager<IUser<int>> userManager, SignInManager<IUser<int>> signInManager, IEmailSender emailSender, ILogger<TokenController> logger, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.logger = logger;
            config = configuration;
        }


        // GET api/token/
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(LoginViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {

                        ICollection<Claim> claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtIssuerOptions:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

                        foreach(var claim in await userManager.GetRolesAsync(user))
                        {
                            claims.Add(new Claim(ClaimTypes.Role, claim));
                        }

                        var token = new JwtSecurityToken(config["JwtIssuerOptions:Issuer"],
                          config["JwtIssuerOptions:Issuer"],
                          claims,
                          notBefore:DateTime.Now,
                          expires: DateTime.Now.AddMinutes(30),
                          signingCredentials: creds);

                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });
                    }
                }
            }

            return BadRequest("Invalid Credendentials");
        }
    }
}