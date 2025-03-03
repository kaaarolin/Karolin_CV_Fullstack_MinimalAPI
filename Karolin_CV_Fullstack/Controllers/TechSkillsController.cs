using Microsoft.AspNetCore.Mvc;
using Karolin_CV_Fullstack.Models;
using System.Collections.Generic;
using Karolin_CV_Fullstack.Data;
using Microsoft.EntityFrameworkCore;

[Route("Tech-skills")]
[ApiController]
public class TechSkillsController : ControllerBase
{
    private readonly CV_DbContext _context;

    public TechSkillsController(CV_DbContext context)
    {
        _context = context;
    }


    // GET: api/TechSkills
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tech_skills>>> GetSkills()
    {
        return await _context.TechSkills.ToListAsync();
    }

    // POST: api/TechSkills
    [HttpPost]
    public async Task<ActionResult<Tech_skills>> PostSkill(Tech_skills skill)
    {
        if (skill == null)
        {
            return BadRequest("Ingen data mottagen.");
        }

        try
        {
            _context.TechSkills.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSkills), new { id = skill.Id }, skill);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid databasuppdatering: {ex.Message}");
            return StatusCode(500, "Ett fel inträffade när datan skulle sparas.");
        }
    }

}

