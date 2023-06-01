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
    internal class UserTaskRepositoryFixture : IDisposable
    {
        public UserTaskRepositoryFixture()
        {
            Context = CreateContext();
            Repository = new UserTaskRepository(Context);
            InitDatabase();
        }

        public DIMSContext Context { get; }
        public IRepository<UserTask> Repository { get; }
        public int UserTaskId { get; private set; }
        private void InitDatabase()
        {
            var entity = Context.UserTasks.Add(new UserTask());
            Context.SaveChanges();
            UserTaskId = entity.Entity.UserTaskId;
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
