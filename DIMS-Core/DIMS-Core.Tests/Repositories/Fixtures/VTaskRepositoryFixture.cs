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
    internal class VTaskRepositoryFixture : IDisposable
    {
        public VTaskRepositoryFixture()
        {
            Context = CreateContext();
            Repository = new VTaskRepository(Context);
            InitDatabase();
        }

        public DIMSContext Context { get; }
        public IReadOnlyRepository<VTask> Repository { get; }
        public int TaskId { get; private set; }
        private void InitDatabase()
        {
            var entity = Context.VTasks.Add(new VTask()
            {
                Name = "Test",
                Description = "Test",
                StartDate = new DateTime(1900, 1, 1),
                DeadlineDate = DateTime.Now
            });
            Context.SaveChanges();
            TaskId = entity.Entity.TaskId;
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
