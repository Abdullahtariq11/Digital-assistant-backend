namespace Digital_assistant_backend;
using Digital_assistant_backend.Models;
using Microsoft.AspNetCore.Mvc;
public class TaskController:Controller
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService=taskService;
    }


    [HttpGet]
    [Route("[controller]/GetbyProjectId/{id:int}")]

    public async Task<IActionResult> getTasksByProjectId ([FromRoute]int id)
    {
        var result= await _taskService.getTasksByProjectId(id);
                if(!result.Success)
        {
            return BadRequest(result.Message);
        }
        return Ok(result.Data);
    }

[HttpPut("UpdateStatus/{projectId:int}/{taskId:int}")]
public async Task<IActionResult> UpdateTaskStatus(int projectId, int taskId, [FromBody] string newStatus)
{
    var result = await _taskService.UpdateTaskStatus(projectId, taskId, newStatus);
    if (!result.Success)
    {
        return BadRequest(result.Message);
    }
    return Ok(result.Data);
}


}
