using Digital_assistant_backend.Models;

namespace Digital_assistant_backend;

public interface IProjectService
{
    public  Task<Service<List<projectDto>>> getAllProjects(string? filterOn=null, string? filterQuerry=null, 
    string? sortBy=null, bool isAscending=true,int pageNumber=1, int pageSize=1000);
    public Task<Service<List<projectDto>>> getByUserId(string id,string? filterOn,string? filterQuerry, string? sortBy,bool isAscending=true);
    
    public Task<Service<createProjectDto>> createProject(createProjectDto project);
    public Task<Service<projectDto>> editProject(projectDto project,int id);
     public Task<Service<projectDto>> DeleteProject(int id);

   
    public Task<Service<projectDto>> getByProjectId(int id);


}
