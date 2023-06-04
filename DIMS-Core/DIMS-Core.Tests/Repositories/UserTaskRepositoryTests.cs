using DIMS_Core.Common.Exceptions;
using DIMS_Core.Tests.Repositories.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsyncTask = System.Threading.Tasks.Task;
using Xunit;
using Microsoft.EntityFrameworkCore;
using DIMS_Core.DataAccessLayer.Models;

namespace DIMS_Core.Tests.Repositories
{
    public class UserTaskRepositoryTests : IDisposable
    {
        public UserTaskRepositoryTests()
        {
            Fixture = new UserTaskRepositoryFixture();
        }

        private UserTaskRepositoryFixture Fixture { get; }

        [Fact]
        public async AsyncTask GetAll_OK()
        {
            //Act
            var userTasks = await Fixture.Repository.GetAll().ToListAsync();

            //Assert
            Assert.NotEmpty(userTasks);
            Assert.Single(userTasks);
        }

        [Fact]
        public async AsyncTask GetById_OK()
        {
            //Act
            var userTask = await Fixture.Repository.GetById(Fixture.UserTaskId);

            //Assert
            Assert.NotNull(userTask);
        }

        [Fact]
        public async AsyncTask GetById_InvalidId_Fail()
        {
            //Arrange
            int id = 0;

            //Assert
            await Assert.ThrowsAsync<InvalidArgumentException>(() => Fixture.Repository.GetById(id));
        }

        [Fact]
        public async AsyncTask GetById_UserTaskNotFound_Fail()
        {
            //Arrange
            int id = int.MaxValue;

            //Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(() => Fixture.Repository.GetById(id));
        }

        [Fact]
        public async AsyncTask Create_OK()
        {
            //Arrange
            var userTask = new UserTask();

            //Act
            var createdTask = await Fixture.Repository.Create(userTask);

            //Assert
            Assert.NotNull(createdTask);
        }

        [Fact]
        public async AsyncTask Create_EmptyUserTask_OK()
        {
            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => Fixture.Repository.Create(null));
        }

        [Fact]
        public async AsyncTask Update_OK()
        {
            //Arrange
            var userTask = new UserTask()
            {
                UserTaskId = Fixture.UserTaskId
            };

            //Act
            var updatedUserTask = Fixture.Repository.Update(userTask);
            await Fixture.Repository.Save();

            //Assert
            Assert.NotNull(updatedUserTask);
        }

        [Fact]
        public void Update_EmptyUserTask_Fail()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => Fixture.Repository.Update(null));
        }

        [Fact]
        public async AsyncTask Delete_OK()
        {
            //Act
            await Fixture.Repository.Delete(Fixture.UserTaskId);
            await Fixture.Repository.Save();

            //Assert
            var deletedUserTask = await Fixture.Context.UserTasks.FindAsync(Fixture.UserTaskId);
            Assert.Null(deletedUserTask);
        }

        [Fact]
        public async AsyncTask Delete_EmptyId_Fail()
        {
            //Arrange
            int id = 0;

            //Assert
            await Assert.ThrowsAsync<InvalidArgumentException>(() => Fixture.Repository.Delete(id));
        }

        public void Dispose()
        {
            Fixture.Dispose();
        }
    }
}
