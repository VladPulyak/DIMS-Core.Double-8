using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Models;
using DIMS_Core.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.Tests.Repositories.Fixtures
{
    internal class TaskTrackRepositoryFixture : IDisposable
    {
        public TaskTrackRepositoryFixture()
        {
            Context = CreateContext();
            Repository = new TaskTrackRepository(Context);

        }
        public DIMSContext Context { get; }
        public IRepository<TaskTrack> Repository { get; }
        public int TaskTrackId { get; set; }
        
        private void InitDatabase()
        {
            var entity = Context.TaskTracks.Add(new TaskTrack
            {
                TrackNote = "Test",
                TrackDate = DateTime.Now
            });
            Context.SaveChanges();
            TaskTrackId = entity.Entity.TaskTrackId;
            entity.State = EntityState.Detached;
        }

        private static DIMSContext CreateContext()
        {
            return new DIMSContext(GetOptions());
        }

        private static DbContextOptions<DIMSContext> GetOptions()
        {
            var builder = new DbContextOptionsBuilder<DIMSContext>().UseInMemoryDatabase(GetDbName());
            return builder.Options;
        }

        private static string GetDbName()
        {
            return $"InMemory_{Guid.NewGuid()}";
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
