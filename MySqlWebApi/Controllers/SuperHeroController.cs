using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlWebApi.Data;
using MySqlWebApi.Models;

namespace MySqlWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        // Make Constrcutor to use Database and ctrl + . press assign and it will make this
        public DataContext db { get; }

        public SuperHeroController(DataContext context)
        {
            db = context;
        }
        [HttpGet]
        public async Task<ActionResult<SuperHero>> Get()
        {
            return Ok(await db.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await db.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero Not Found");
            }
            else
            {
                return Ok(hero);
            }

        }

        [HttpPost]
        public async Task<ActionResult<SuperHero>> AddHero(SuperHero Hero)
        {
            db.SuperHeroes.Add(Hero);
            await db.SaveChangesAsync();
            return Ok(await db.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await db.SuperHeroes.FindAsync(request.Id);
            if (hero == null)
            {
                return BadRequest("Hero Not Found");
            }
            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            await db.SaveChangesAsync();

            return Ok(await db.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = await db.SuperHeroes.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("Hero Not Found");
            }
            else
            {
                db.SuperHeroes.Remove(dbHero);
                await db.SaveChangesAsync();
                return Ok(await db.SuperHeroes.ToListAsync());
            }

        }
    }
}
