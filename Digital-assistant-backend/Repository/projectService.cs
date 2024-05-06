using Digital_assistant_backend.Data;
using Digital_assistant_backend.Migrations;
using Digital_assistant_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Digital_assistant_backend;

public class projectService : IProjectService
{
    private readonly ManagementDbContext _dbcontext;

    public projectService(ManagementDbContext dbContext)
    {
        _dbcontext = dbContext;
    }
    public async Task<Service<createProjectDto>> createProject(createProjectDto project)
    {
        if (project == null) return Service<createProjectDto>.failure("incorrect data");
        var user = _dbcontext.Users.Find(project.UserId);
        if (user == null) return Service<createProjectDto>.failure("invalid user");
        var tasks = new List<ProjectTask>();
        if (project.Tasks!= null )
        {   
            foreach (var task in project.Tasks)
            {
                var newtask = new ProjectTask
                {
                    Name = task.Name,
                    Status = task.Status,
                };
                tasks.Add(newtask);
            }
        }



        var newProject = new Project
        {
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            Priority = project.Priority,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            UserId = project.UserId,
            User = user,
            Tasks = tasks,
        };
        await _dbcontext.Projects.AddAsync(newProject);
        await _dbcontext.SaveChangesAsync();
        return Service<createProjectDto>.success(project);
    }

    public Task<Service<Project>> editProject(Project project)
    {
        throw new NotImplementedException();
    }

    public async Task<Service<List<projectDto>>> getAllProjects()
    {
        var projects=await _dbcontext.Projects.ToListAsync();
        if(projects==null) return Service<List<projectDto>>.failure("no projects exist");

        var newProjects=new List<projectDto>();
        foreach(var project in projects)
        {
            var newProject= new projectDto
            {
                Name=project.Name,
                Description=project.Description,
                Status=project.Status,
                StartDate=project.StartDate,
                EndDate=project.EndDate,
                Priority=project.Priority,
            };
            newProjects.Add(newProject);
        }
        return Service<List<projectDto>>.success(newProjects);
    }

    public async Task<Service<List<projectDto>>> getByUserId(int id)
    {
        var projects= await _dbcontext.Projects.Where(x=>x.UserId==id).ToListAsync();
         if(projects==null|| projects.Count==0) return Service<List<projectDto>>.failure("no projects exist");

        var newProjects=new List<projectDto>();
        foreach(var project in projects)
        {
            var newProject= new projectDto
            {
                Name=project.Name,
                Description=project.Description,
                Status=project.Status,
                StartDate=project.StartDate,
                EndDate=project.EndDate,
                Priority=project.Priority,
            };
            newProjects.Add(newProject);
        }
        return Service<List<projectDto>>.success(newProjects);      
    }

}
