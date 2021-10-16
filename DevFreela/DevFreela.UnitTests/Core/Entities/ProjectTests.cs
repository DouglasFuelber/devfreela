using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using Xunit;

namespace DevFreela.UnitTests.Core.Entities
{
    public class ProjectTests
    {
        [Fact]
        public void ProjectIsCreated_Executed_ProjectIsStarted()
        {
            var project = new Project("Test project", "Test project description", 1, 2, 500);

            project.Start();

            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);
        }

        [Fact]
        public void ProjectIsCreatedInProgressOrSuspended_Executed_ProjectIsCanceled()
        {
            var project = new Project("Test project", "Test project description", 1, 2, 500);

            project.Cancel();

            Assert.Equal(ProjectStatusEnum.Canceled, project.Status);
        }

        [Fact]
        public void ProjectIsInProgress_Executed_ProjectIsFinished()
        {
            var project = new Project("Test project", "Test project description", 1, 2, 500);

            project.Start();
            project.Finish();

            Assert.Equal(ProjectStatusEnum.Finished, project.Status);
            Assert.NotNull(project.FinishedAt);
        }

        [Fact]
        public void ProjectIsCreated_Executed_ProjectPropertyIsUpdated()
        {
            var project = new Project("Test project", "Test project description", 1, 2, 500);
            string newTitle = "Project Title Updated";
            string newDescription = "Project Description Updated";
            decimal newTotalCost = 1000;

            project.Update(newTitle, newDescription, newTotalCost);

            Assert.Equal(project.Title, newTitle);
            Assert.Equal(project.Description, newDescription);
            Assert.Equal(project.TotalCost, newTotalCost);
        }
    }
}
