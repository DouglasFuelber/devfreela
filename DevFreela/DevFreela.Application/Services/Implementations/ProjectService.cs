using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        public int Create(NewProjectInputModel project)
        {
            throw new NotImplementedException();
        }

        public void CreateComment(CreateCommentInputModel comment)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Finish(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            throw new NotImplementedException();
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Start(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdateProjectInputModel project)
        {
            throw new NotImplementedException();
        }
    }
}
