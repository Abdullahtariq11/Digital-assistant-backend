namespace Digital_assistant_backend;
using Digital_assistant_backend.Data;

public interface ITaskService
{
    public Task<Service<List<taskDto>>> getTasksByProjectId(int id);
     public  Task<Service<bool>> UpdateTaskStatus(int projectId,int taskId, string newStatus);

}
