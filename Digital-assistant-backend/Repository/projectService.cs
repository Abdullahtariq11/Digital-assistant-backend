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

    public async Task<Service<projectDto>> DeleteProject(int id)
    {
        var project=await _dbcontext.Projects.FirstOrDefaultAsync(x=>x.UserId==1&& x.Id==id);
        if( project==null) return Service<projectDto>.failure("no projects exist or incorrect data");
        _dbcontext.Projects.Remove(project);
        await _dbcontext.SaveChangesAsync();

        var projectDeleted= new projectDto
        {
                id=project.Id,
                Name=project.Name,
                Description=project.Description,
                Status=project.Status,
                StartDate=project.StartDate,
                EndDate=project.EndDate,
                Priority=project.Priority,
        };


       return Service<projectDto>.success(projectDeleted);
    }

    public async Task<Service<projectDto>> editProject(projectDto project,int id)
    {   
        var projectRecieved=await _dbcontext.Projects.FirstOrDefaultAsync(x=>x.UserId==1&& x.Id==id);
         if(project==null || projectRecieved==null) return Service<projectDto>.failure("no projects exist or incorrect data");
         projectRecieved.Name=project.Name;
         projectRecieved.Description=project.Description;
         projectRecieved.Status=project.Status;
         projectRecieved.Priority=project.Priority;
         projectRecieved.StartDate=project.StartDate;
         projectRecieved.EndDate=project.EndDate;
         await _dbcontext.SaveChangesAsync();

         var newProject= new projectDto
         {
            id=project.id,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            Priority = project.Priority,
            StartDate = project.StartDate,
            EndDate = project.EndDate
         };

         return Service<projectDto>.success(newProject);     
    }

    public async Task<Service<List<projectDto>>> getAllProjects(string? filterOn=null, string? filterQuerry=null,string? sortBy=null, bool isAscending=true,int pageNumber=1, int pageSize=1000)
    {
        //var projects=await _dbcontext.Projects.ToListAsync();

        var queryableProjects=  _dbcontext.Projects.AsQueryable();
        
        //filtering
        if(string.IsNullOrWhiteSpace(filterOn)==false&&string.IsNullOrWhiteSpace(filterQuerry)==false){
            if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase)){
                queryableProjects=queryableProjects.Where(x=>x.Name.Contains(filterQuerry));
            }
            else if(filterOn.Equals("Description",StringComparison.OrdinalIgnoreCase)){
                queryableProjects=queryableProjects.Where(x=>x.Description.Contains(filterQuerry));
            }
        }
        //Sorting
        if(string.IsNullOrWhiteSpace(sortBy)==false ){
            if(sortBy.Equals("Name",StringComparison.OrdinalIgnoreCase)){
                queryableProjects=isAscending? queryableProjects.OrderBy(x=>x.Name): queryableProjects.OrderByDescending(x=>x.Name);
            }
            else if(sortBy.Equals("StartDate",StringComparison.OrdinalIgnoreCase)){
                queryableProjects=isAscending? queryableProjects.OrderBy(x=>x.StartDate): queryableProjects.OrderByDescending(x=>x.StartDate);
            }
            else if(sortBy.Equals("EndDate",StringComparison.OrdinalIgnoreCase)){
                queryableProjects=isAscending? queryableProjects.OrderBy(x=>x.EndDate): queryableProjects.OrderByDescending(x=>x.EndDate);
            }
        }
        //Pagination
        var skipResult= (pageNumber-1)*pageSize;


        var projects= await queryableProjects.Skip(skipResult).Take(pageSize).ToListAsync();

        if(projects==null) return Service<List<projectDto>>.failure("no projects exist");

        var newProjects=new List<projectDto>();
        foreach(var project in projects)
        {
            var newProject= new projectDto
            {
                id=project.Id,
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

    public async Task<Service<projectDto>> getByProjectId(int id)
    {
        var project= await _dbcontext.Projects.FirstOrDefaultAsync(x=>x.UserId==1&& x.Id==id);

       
         if(project==null) return Service<projectDto>.failure("no projects exist");
          var newProject= new projectDto
            {
                id=project.Id,
                Name=project.Name,
                Description=project.Description,
                Status=project.Status,
                StartDate=project.StartDate,
                EndDate=project.EndDate,
                Priority=project.Priority,
            };

        return Service<projectDto>.success(newProject);
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
