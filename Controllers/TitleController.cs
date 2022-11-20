using Andals.API.Data;
using Andals.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Andals.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TitleController : Controller
    {
        private readonly AndalsDbContext andalsDbContext;

        public TitleController(AndalsDbContext andalsDbContext)
        {
            this.andalsDbContext = andalsDbContext;
        }
        //Get All Title
        [HttpGet]
        public async Task<IActionResult> GetAllTitle() 
        {
            var titles =  await andalsDbContext.Titles.ToListAsync();
            return Ok(titles);
        }

        //Get Sigle Title

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetTitle")]
        public async Task<IActionResult> GetTitle([FromRoute] int id)
        {
            var title = await andalsDbContext.Titles.FirstOrDefaultAsync(x => x.ID == id);
            if (title != null)
            {
                return Ok(title);
            }
            return NotFound("Title not Foud");
        }

        //add Title
        [HttpPost]
        public async Task<IActionResult> AddTitle([FromBody] CreateTitleDto request)
        {
            var newTitle = new Title
            {
                Code= request.Code,
                Name= request.Name

            };
            //title.ID = Guid.NewGuid();
            await andalsDbContext.Titles.AddAsync(newTitle);
            await andalsDbContext.SaveChangesAsync();

            return Ok(newTitle);
        }

        //update Title
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateTitle([FromRoute] int id, CreateTitleDto request)
        {
            var existingTitle = await andalsDbContext.Titles.FirstOrDefaultAsync(x =>x.ID == id);
            if (existingTitle != null)
            {
                existingTitle.Code = request.Code;
                existingTitle.Name = request.Name;
                await andalsDbContext.SaveChangesAsync();
                return Ok(existingTitle);
            }
            return NotFound("Title not found");
        }

        //Delete Title
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateTitle([FromRoute] int id)
        {
            var existingTitle = await andalsDbContext.Titles.FirstOrDefaultAsync(x => x.ID == id);
            if (existingTitle != null)
            {
               andalsDbContext.Remove(existingTitle);
                await andalsDbContext.SaveChangesAsync();
                return Ok(existingTitle);
            }
            return NotFound("Title not found");
        }

    }
}
