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


    [HttpPut]
    [Route("[controller]/editProject/{id:int}")]
    public async Task<IActionResult> editProject([FromBody] projectDto project,[FromRoute]int id)
    {
        var result=await _projectService.editProject(project,id);
                        if(!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Data);

    }

    [HttpDelete]
    [Route("[controller]/DeleteProjectbyId/{id:int}")]
     public async Task<IActionResult> DeleteProjectbyId([FromRoute]int id)
    {
        var result= await _projectService.DeleteProject(id);
                if(!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Data);
    }


    [HttpGet]
    [Route("[controller]/getByProjectId/{id:int}")]
    public async Task<IActionResult> getByProjectId([FromRoute]int id)
    {
        var result= await _projectService.getByProjectId(id);
                if(!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Data);
    }
}
