using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TrackingSystem.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TrackingSystemBLayer.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TrackingSystem.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DeviceController : Controller
    {
        private readonly UserManager<IUser<int>> userManager;
        private readonly SignInManager<IUser<int>> signInManager;
        private readonly IEmailSender emailSender;
        private readonly IConfiguration config;
        private readonly ILogger logger;

        public DeviceController(UserManager<IUser<int>> userManager, IEmailSender emailSender, ILogger<TokenController> logger, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.logger = logger;
            config = configuration;
        }

        // GET: api/Device
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Device/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Device
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Device/5
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
