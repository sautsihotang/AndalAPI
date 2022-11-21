using Andals.API.Data;
using Andals.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Andals.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionController: Controller
    {
        private readonly AndalsDbContext andalsDbContext;
        public PositionController(AndalsDbContext andalsDbContext) 
        {
            this.andalsDbContext = andalsDbContext;
        }

        //Get All Position
        [HttpGet]
        public async Task<IActionResult> GetAllPosition()
        {

            var GetPosition = (from p in andalsDbContext.Positions
                             join t in andalsDbContext.Titles
                             on p.TitleID equals t.ID
                           

                             select new TitlePositionDto
                             {
                                 ID = p.ID,
                                 CodePosition = p.Code,
                                 NamePosition= p.Name,
                                 NameTitle  =   t.Name
                             }).ToList();

            return Ok(GetPosition);
        }

        //Get Sigle Title

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetPosition")]
        public async Task<IActionResult> GetPosition([FromRoute] int id)
        {
            var position = await andalsDbContext.Positions.FirstOrDefaultAsync(x => x.ID == id);
            if (position != null)
            {
                return Ok(position);
            }
            return NotFound("Position not Foud");
        }

        //add Position
        [HttpPost]
        public async Task<IActionResult> AddPosition([FromBody] CreatePositionDto request)
        {
            var newPosition = new Position
            {
                Code = request.Code,
                Name = request.Name,
                TitleID= request.TitleID,

            };
            await andalsDbContext.Positions.AddAsync(newPosition);
            await andalsDbContext.SaveChangesAsync();

            return Ok(newPosition);
        }

        //update Position
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdatePosition([FromRoute] int id, CreatePositionDto request)
        {
            var existingPosition = await andalsDbContext.Positions.FirstOrDefaultAsync(x => x.ID == id);
            if (existingPosition != null)
            {
                existingPosition.Code = request.Code;
                existingPosition.Name = request.Name;
                existingPosition.TitleID = request.TitleID;
                await andalsDbContext.SaveChangesAsync();
                return Ok(existingPosition);
            }
            return NotFound("Position not found");
        }

        //Delete Position
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeletePosition([FromRoute] int id)
        {
            var existingPosition = await andalsDbContext.Positions.FirstOrDefaultAsync(x => x.ID == id);
            if (existingPosition != null)
            {
                andalsDbContext.Remove(existingPosition);
                await andalsDbContext.SaveChangesAsync();
                return Ok(existingPosition);
            }
            return NotFound("Position not found");
        }
    }
}
