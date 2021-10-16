using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllSkillsQueryHandlerTests
    {
        [Fact]
        public async Task ThreeSkillsExist_Executed_ReturnThreeSkillViewModels()
        {
            // Arrange
            var skills = new List<Skill> {
                new Skill("Skill 1"),
                new Skill("Skill 2"),
                new Skill("Skill 3"),
            };

            var skillRepositoryMock = new Mock<ISkillRepository>();
            skillRepositoryMock.Setup(s => s.GetAllAsync().Result).Returns(skills);

            var getAllSkillsQuery = new GetAllSkillsQuery();
            var getAllSkillsQueryHandler = new GetAllSkillsQueryHandler(skillRepositoryMock.Object);

            // Act
            var skillsViewModelList = await getAllSkillsQueryHandler.Handle(getAllSkillsQuery, new CancellationToken());

            // Assert
            Assert.NotNull(skillsViewModelList);
            Assert.NotEmpty(skillsViewModelList);
            Assert.Equal(skills.Count, skillsViewModelList.Count);

            skillRepositoryMock.Verify(p => p.GetAllAsync().Result, Times.Once);
        }
    }
}
