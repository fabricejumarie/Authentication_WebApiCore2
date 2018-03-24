using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lab.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Lab.Data.Model;

namespace WebApplication1.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly LabDbContext _dbContext;
        private readonly UserManager<LabUser> _userManager;

        public ProductsController(LabDbContext dbContext, UserManager<LabUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        // GET: api/Product
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Products.ToList());
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Product
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Product/5
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
