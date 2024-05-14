
using Digital_assistant_backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Digital_assistant_backend;

public class taskService : ITaskService
{
    private readonly ManagementDbContext _dbcontext;
    public taskService(ManagementDbContext dbContext)
    {
        _dbcontext=dbContext;
    }
    public async Task<Service<List<taskDto>>> getTasksByProjectId(int id)
    {
        var tasks= await _dbcontext.Tasks.Where(x=>x.ProjectId==id).ToListAsync();
        var taskList=new List<taskDto>();
        foreach(var task in tasks)
        {
        var dtoTask=new taskDto
        {
            id=task.Id,
            Name=task.Name,
            Status=task.Status,
        };
        taskList.Add(dtoTask);
        }

        return Service<List<taskDto>>.success(taskList);

    }
    public async Task<Service<bool>> UpdateTaskStatus(int projectId,int taskId, string newStatus)
    {
             var tasks= await _dbcontext.Tasks.Where(x=>x.ProjectId==projectId).ToListAsync();
            var task = tasks.FirstOrDefault(x=>x.Id==taskId);
            if (task == null)
            {
                return Service<bool>.failure("Task not found");
            }

            // Update task status
            task.Status = newStatus;
            _dbcontext.Tasks.Update(task);
            await _dbcontext.SaveChangesAsync();

            return Service<bool>.success(true);
    }
        
}
