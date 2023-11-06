using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.DomainModels;
using NZWalksAPI.Models.DTO;
using System.Net.WebSockets;

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
            var regionsDomain = DbContext.Regions.ToList();


            var regionsdto = new List<RegionsDTO>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsdto.Add(new RegionsDTO()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    ShortName = regionDomain.ShortName,
                    Regionimageurl = regionDomain.Regionimageurl
                });

            }

            return Ok(regionsdto);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var regions = DbContext.Regions.Find(id);

            var regionDomain = DbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionsdto = new RegionsDTO
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                ShortName = regionDomain.ShortName,
                Regionimageurl = regionDomain.Regionimageurl
            };

            return Ok(regionsdto);
        }


        [HttpPost]
        public IActionResult Insert([FromBody] AddRegionsDTO addRegionsDTO)
        {
            var regionDominModel = new Region
            {
                ShortName = addRegionsDTO.ShortName,
                Name = addRegionsDTO.Name,
                Regionimageurl = addRegionsDTO.Regionimageurl,
            };

            DbContext.Regions.Add(regionDominModel);
            DbContext.SaveChanges();

            var regionDto = new RegionsDTO
            {
                Id = regionDominModel.Id,
                Name = regionDominModel.Name,
                ShortName = regionDominModel.ShortName,
                Regionimageurl = regionDominModel.Regionimageurl
            };

            return CreatedAtAction(nameof(GetById),new {id = regionDto.Id}, regionDto);
        }
    }
}
