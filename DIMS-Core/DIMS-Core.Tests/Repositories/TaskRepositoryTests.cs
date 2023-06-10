using DIMS_Core.Common.Exceptions;
using DIMS_Core.Tests.Repositories.Fixtures;
using Microsoft.EntityFrameworkCore;
using System;
using DIMS_Core.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;
using AsyncTask = System.Threading.Tasks.Task;

namespace DIMS_Core.Tests.Repositories
{
    public class TaskRepositoryTests :IDisposable
    {
        public TaskRepositoryTests()
        {
            Fixture = new TaskRepositoryFixture();
        }

        private TaskRepositoryFixture Fixture { get; }

        [Fact]
        public async AsyncTask GetAll_OK()
        {
            //Act
            var tasks = await Fixture.Repository.GetAll().ToListAsync();

            //Assert
            Assert.NotEmpty(tasks);
            Assert.Single(tasks);
        }

        [Fact]
        public async AsyncTask GetById_OK()
        {
            //Act
            var task = await Fixture.Repository.GetById(Fixture.TaskId);

            //Assert
            Assert.NotNull(task);
            Assert.Equal("Test", task.Name);
            Assert.Equal("Test Task", task.Description);
            Assert.Equal(new DateTime(1900, 1, 1).ToString(), task.StartDate.ToString());
            Assert.Equal(DateTime.Now.ToString(), task.DeadlineDate.ToString());
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
        public async AsyncTask GetById_TaskNotFound_Fail()
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
            var task = new Task()
            {
                Name = "Test",
                Description = "Test Test",
                StartDate = DateTime.Now,
                DeadlineDate = DateTime.MinValue
            };

            //Act
            var createdTask = await Fixture.Repository.Create(task);

            //Assert
            Assert.NotNull(createdTask);
            Assert.Equal(task.Name, createdTask.Name);
            Assert.Equal(task.Description, createdTask.Description);
            Assert.Equal(task.StartDate, createdTask.StartDate);
            Assert.Equal(task.DeadlineDate, createdTask.DeadlineDate);
        }

        [Fact]
        public async AsyncTask Create_EmptyTask_OK()
        {
            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => Fixture.Repository.Create(null));
        }

        [Fact]
        public async AsyncTask Update_OK()
        {
            //Arrange
            var task = new Task()
            {
                TaskId = Fixture.TaskId,
                Name = "UpdatedTest",
                Description = "UpdatedTest",
                StartDate = DateTime.Now,
                DeadlineDate = DateTime.MaxValue
            };

            //Act
            var updatedTask = Fixture.Repository.Update(task);
            await Fixture.Repository.Save();

            //Assert
            Assert.NotNull(updatedTask);
            Assert.Equal(task.Name, updatedTask.Name);
            Assert.Equal(task.Description, updatedTask.Description);
            Assert.Equal(task.StartDate, updatedTask.StartDate);
            Assert.Equal(task.DeadlineDate, updatedTask.DeadlineDate);
        }

        [Fact]
        public void Update_EmptyTask_Fail()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => Fixture.Repository.Update(null));
        }

        [Fact]
        public async AsyncTask Delete_OK()
        {
            //Act
            await Fixture.Repository.Delete(Fixture.TaskId);
            await Fixture.Repository.Save();

            //Assert
            var deletedTask = await Fixture.Context.Tasks.FindAsync(Fixture.TaskId);
            Assert.Null(deletedTask);
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
