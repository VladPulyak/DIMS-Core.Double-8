using DIMS_Core.Common.Exceptions;
using DIMS_Core.Tests.Repositories.Fixtures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsyncTask = System.Threading.Tasks.Task;
using Xunit;
using DIMS_Core.DataAccessLayer.Models;

namespace DIMS_Core.Tests.Repositories
{
    public class TaskStateRepositoryTests : IDisposable
    {
        public TaskStateRepositoryTests()
        {
            Fixture = new TaskStateRepositoryFixture();
        }

        private TaskStateRepositoryFixture Fixture { get; }

        [Fact]
        public async AsyncTask GetAll_OK()
        {
            //Act
            var taskStates = await Fixture.Repository.GetAll().ToListAsync();

            //Assert
            Assert.NotEmpty(taskStates);
            Assert.Single(taskStates);
        }

        [Fact]
        public async AsyncTask GetById_OK()
        {
            //Act
            var taskState = await Fixture.Repository.GetById(Fixture.StateId);

            //Assert
            Assert.NotNull(taskState);
            Assert.Equal("Test", taskState.StateName);
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
        public async AsyncTask GetById_TaskStateNotFound_Fail()
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
            var taskState = new TaskState()
            {
                StateName = "Test"
            };

            //Act
            var createdTaskState = await Fixture.Repository.Create(taskState);

            //Assert
            Assert.NotNull(createdTaskState);
            Assert.Equal(taskState.StateName, createdTaskState.StateName);
        }

        [Fact]
        public async AsyncTask Create_EmptyTaskState_OK()
        {
            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => Fixture.Repository.Create(null));
        }

        [Fact]
        public async AsyncTask Update_OK()
        {
            //Arrange
            var taskState = new TaskState()
            {
                StateId = Fixture.StateId,
                StateName = "Test Test"
            };

            //Act
            var updatedTaskState = Fixture.Repository.Update(taskState);
            await Fixture.Repository.Save();

            //Assert
            Assert.NotNull(updatedTaskState);
            Assert.Equal(taskState.StateName, updatedTaskState.StateName);
        }

        [Fact]
        public void Update_EmptyTaskState_Fail()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => Fixture.Repository.Update(null));
        }

        [Fact]
        public async AsyncTask Delete_OK()
        {
            //Act
            await Fixture.Repository.Delete(Fixture.StateId);
            await Fixture.Repository.Save();

            //Assert
            var deletedTaskState = await Fixture.Context.TaskStates.FindAsync(Fixture.StateId);
            Assert.Null(deletedTaskState);
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
