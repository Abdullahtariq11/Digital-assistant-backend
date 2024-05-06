namespace Digital_assistant_backend;

using Digital_assistant_backend.Models;
using Microsoft.AspNetCore.Mvc;

public class ProjectController : Controller
{
    private readonly IProjectService _projectService;
    public ProjectController(IProjectService projectService)
    {
        _projectService=projectService;
    }

    [HttpPost]
    [Route("[controller]/Create")]
    public async Task<IActionResult> CreateProject([FromBody] createProjectDto project)
    {
        var result= await _projectService.createProject(project);
        if(!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Data);
    }

    [HttpGet]
    [Route("[controller]/GetAll")]
    public async Task<IActionResult> GetALlProjects()
    {
        var result= await _projectService.getAllProjects();
                if(!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Data);
    }

    [HttpGet]
    [Route("[controller]/GetbyId/{id:int}")]
    public async Task<IActionResult> GetByUserId([FromRoute]int id)
    {
        var result= await _projectService.getByUserId(id);
                if(!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Data);
    }
}
