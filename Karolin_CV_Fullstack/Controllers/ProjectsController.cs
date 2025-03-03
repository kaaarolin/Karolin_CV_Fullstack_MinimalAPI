using Microsoft.AspNetCore.Mvc;
using Karolin_CV_Fullstack.Models;
using System.Collections.Generic;

[Route("/Projects")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private static readonly List<Projects> projects = new()
    {
        new Projects { Id = 1, Name = "Project A", Description = "Description A" },
        new Projects { Id = 2, Name = "Project B", Description = "Description B" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Projects>> GetProjects()
    {
        return Ok(projects);
    }
}
