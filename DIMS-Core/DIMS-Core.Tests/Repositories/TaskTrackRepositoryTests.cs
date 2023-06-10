using DIMS_Core.Common.Exceptions;
using DIMS_Core.DataAccessLayer.Models;
using DIMS_Core.Tests.Repositories.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsyncTask = System.Threading.Tasks.Task;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace DIMS_Core.Tests.Repositories
{
    public class TaskTrackRepositoryTests : IDisposable
    {
        public TaskTrackRepositoryTests()
        {
            Fixture = new TaskTrackRepositoryFixture();
        }

        private TaskTrackRepositoryFixture Fixture { get; }

        [Fact]
        public async AsyncTask GetAll_OK()
        {
            //Act
            var taskTracks = await Fixture.Repository.GetAll().ToListAsync();

            //Assert
            Assert.NotEmpty(taskTracks);
            Assert.Single(taskTracks);
        }

        [Fact]
        public async AsyncTask GetById_OK()
        {
            //Act
            var taskTrack = await Fixture.Repository.GetById(Fixture.TaskTrackId);

            //Assert
            Assert.NotNull(taskTrack);
            Assert.Equal("Test", taskTrack.TrackNote);
            Assert.Equal(DateTime.Now.ToString(), taskTrack.TrackDate.ToString());
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
        public async AsyncTask GetById_TaskTrackNotFound_Fail()
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
            var taskTrack = new TaskTrack()
            {
                TrackNote = "Test",
                TrackDate = DateTime.Now
            };

            //Act
            var createdTaskTrack = await Fixture.Repository.Create(taskTrack);

            //Assert
            Assert.NotNull(createdTaskTrack);
            Assert.Equal(createdTaskTrack.TrackNote, taskTrack.TrackNote);
            Assert.Equal(createdTaskTrack.TrackDate.ToString(), taskTrack.TrackDate.ToString());
        }

        [Fact]
        public async AsyncTask Create_EmptyTaskTrack_OK()
        {
            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => Fixture.Repository.Create(null));
        }

        [Fact]
        public async AsyncTask Update_OK()
        {
            //Arrange
            var taskTrack = new TaskTrack()
            {
                TrackNote = "Test test",
                TrackDate = DateTime.MinValue
            };

            //Act
            var updatedTaskTrack = Fixture.Repository.Update(taskTrack);
            await Fixture.Repository.Save();

            //Assert
            Assert.NotNull(updatedTaskTrack);
            Assert.Equal(taskTrack.TrackNote, updatedTaskTrack.TrackNote);
            Assert.Equal(taskTrack.TrackDate.ToString(), updatedTaskTrack.TrackDate.ToString());
        }

        [Fact]
        public void Update_EmptyTaskTrack_Fail()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => Fixture.Repository.Update(null));
        }

        [Fact]
        public async AsyncTask Delete_OK()
        {
            //Act
            await Fixture.Repository.Delete(Fixture.TaskTrackId);
            await Fixture.Repository.Save();

            //Assert
            var deletedTaskTrack = await Fixture.Context.TaskTracks.FindAsync(Fixture.TaskTrackId);
            Assert.Null(deletedTaskTrack);
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
