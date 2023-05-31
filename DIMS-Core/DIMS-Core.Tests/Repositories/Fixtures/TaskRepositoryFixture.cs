using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Models;
using DIMS_Core.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIMS_Core.Tests.Repositories.Fixtures
{
    internal class TaskRepositoryFixture : IDisposable
    {
        public TaskRepositoryFixture()
        {
            Context = CreateContext();
            Repository = new TaskRepository(Context);
            InitDatabase();
        }
        public IRepository<Task> Repository { get; }

        public DIMSContext Context { get; }

        public int TaskId { get; private set; }

        private void InitDatabase()
        {
            var entity = Context.Tasks.Add(new Task()
            {
                Name = "Test",
                Description = "Test Task",
                StartDate = new DateTime(1900, 1, 1),
                DeadlineDate = DateTime.Now
            });
            TaskId = entity.Entity.TaskId;
            Context.SaveChanges();
            entity.State = EntityState.Detached;
        }

        private static DIMSContext CreateContext()
        {
            return new DIMSContext(GetOptions());
        }

        private static DbContextOptions<DIMSContext> GetOptions()
        {
            var builder = new DbContextOptionsBuilder<DIMSContext>().UseInMemoryDatabase(GetDBName());
            return builder.Options;
        }

        private static string GetDBName()
        {
            return $"InMemory_{Guid.NewGuid()}";
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
