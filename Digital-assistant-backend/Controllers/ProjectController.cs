namespace Digital_assistant_backend;
using Digital_assistant_backend.CustomActionFilters;
using Digital_assistant_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
    
public class ProjectController : Controller
{
    private readonly IProjectService _projectService;
    public ProjectController(IProjectService projectService)
    {
        _projectService=projectService;
    }

    [HttpPost]
    [ValidateModel]
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
    public async Task<IActionResult> GetALlProjects([FromQuery] string? filterOn,[FromQuery] string? filterQuerry,
     [FromQuery] string? sortBy,[FromQuery] bool isAscending=true,
     [FromQuery] int pageNumber=1,[FromQuery] int pageSize=1000)
    {
        var result= await _projectService.getAllProjects(filterOn,filterQuerry,sortBy,isAscending,pageNumber,pageSize);
                if(!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Data);
    }

    //Get All Projects By User Id
    [HttpGet]
    [Route("[controller]/GetbyId/{id:int}")]
    public async Task<IActionResult> GetbyId([FromRoute]int id,[FromQuery] string? filterOn,[FromQuery] string? filterQuerry,
    [FromQuery] string? sortBy,[FromQuery]bool isAscending=true)
    {
        var result= await _projectService.getByUserId(id,filterOn,filterQuerry,sortBy,isAscending);
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
