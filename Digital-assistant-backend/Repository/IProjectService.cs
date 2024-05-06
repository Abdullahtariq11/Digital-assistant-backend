using Digital_assistant_backend.Models;

namespace Digital_assistant_backend;

public interface IProjectService
{
    public  Task<Service<List<projectDto>>> getAllProjects();
    public Task<Service<List<projectDto>>> getByUserId(int id);
    
    public Task<Service<createProjectDto>> createProject(createProjectDto project);

    public Task<Service<Project>> editProject(Project project);



}
