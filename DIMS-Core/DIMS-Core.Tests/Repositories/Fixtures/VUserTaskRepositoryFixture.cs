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
    internal class VUserTaskRepositoryFixture : IDisposable
    {
        public VUserTaskRepositoryFixture()
        {
            Context = CreateContext();
            Repository = new VUserTaskRepository(Context);
            InitDatabase();
        }

        public DIMSContext Context { get; }
        public IReadOnlyRepository<VUserTask> Repository { get; }
        private void InitDatabase()
        {
            var entity = Context.VUserTasks.Add(new VUserTask()
            {
                StateName = "Test",
                Description = "Test",
                StartDate = new DateTime(1900, 1, 1),
                DeadlineDate = DateTime.Now,
                TaskName = "Test"
            });
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
