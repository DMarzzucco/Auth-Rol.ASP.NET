using Auth_Rol.ASP.NET.Auth.Services.Interfaces;
using Auth_Rol.ASP.NET.Project.DTO;
using Auth_Rol.ASP.NET.Project.Model;
using Auth_Rol.ASP.NET.Project.Repository.Interface;
using Auth_Rol.ASP.NET.Project.Service.Interface;
using Auth_Rol.ASP.NET.Users.DTO;
using Auth_Rol.ASP.NET.Users.Enums;
using Auth_Rol.ASP.NET.Users.Model;
using Auth_Rol.ASP.NET.Users.Repository.Interface;
using AutoMapper;

namespace Auth_Rol.ASP.NET.Project.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IUserProjectRepository _userProjectRepository;
        private readonly IAuthServices _authService;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository repository, IAuthServices authServices, IMapper mapper, IUserProjectRepository userProjectRepository)
        {
            this._repository = repository;
            this._authService = authServices;
            this._mapper = mapper;
            this._userProjectRepository = userProjectRepository;
        }
        public async Task<UsersProjectModel> saveProject(CreateProjectDTO body)
        {
            var user = await this._authService.GetUserProfile();

            var project = this._mapper.Map<ProjectModel>(body);
            await this._repository.SaveProjectAsync(project);

            var relations = new UsersProjectDTO
            {
                AccessLevel = AccesLevel.OWNER,
                User = user,
                Project = project
            };
            var userProject = this._mapper.Map<UsersProjectModel>(relations);

            await this._userProjectRepository.AddChangeAsync(userProject);
            return userProject;
        }

        public async Task delteProject(int id)
        {
            var project = await this._repository.FinByIdAsync(id);
            if (project == null) throw new KeyNotFoundException("project not found");

            await this._repository.RemoveAsync(project);
        }

        public async Task<IEnumerable<ProjectModel>> getAllProject()
        {
            return await this._repository.ToListAsync();
        }

        public async Task<ProjectModel> getProjectById(int id)
        {
            var project = await this._repository.FinByIdAsync(id);
            if (project == null) throw new KeyNotFoundException("project not found");

            return project;
        }

        public async Task<ProjectModel> updateProject(int id, UpdateProjectDTO body)
        {
            var data = await this._repository.FinByIdAsync(id);
            if (data == null) throw new KeyNotFoundException("Project not found");

            this._mapper.Map(body, data);

            await this._repository.UpdateAsync(data);
            return data;
        }
    }
}
