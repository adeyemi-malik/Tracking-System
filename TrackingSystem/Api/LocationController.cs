using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using TrackingSystem.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using TrackingSystemBLayer.Authentication;
using TrackingSystem.Models.LocationViewModels;

namespace TrackingSystem.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private readonly UserManager<IUser<int>> userManager;
        private readonly SignInManager<IUser<int>> signInManager;
        private readonly IEmailSender emailSender;
        private readonly IConfiguration config;
        private readonly ILogger logger;

        public LocationController(UserManager<IUser<int>> userManager, SignInManager<IUser<int>> signInManager, IEmailSender emailSender, ILogger<TokenController> logger, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.logger = logger;
            config = configuration;
        }

        // GET: api/Locations
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Locations
        [HttpPost]
        public void Post(Location value)
        {

        }
        
        // PUT: api/Locations/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
