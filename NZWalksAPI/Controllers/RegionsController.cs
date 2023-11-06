using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.DomainModels;

namespace NZWalksAPI.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NzWalksDbContext DbContext;

        public RegionsController(NzWalksDbContext DbContext)
        {
            this.DbContext = DbContext;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = DbContext.Regions.ToList();

            return Ok(regions);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var regions = DbContext.Regions.Find(id);

            var regions = DbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regions == null)
            {
                return NotFound();
            }    

            return Ok(regions);
        }
    }
}
