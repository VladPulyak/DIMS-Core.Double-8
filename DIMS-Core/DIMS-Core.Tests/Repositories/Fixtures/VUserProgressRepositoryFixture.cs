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
    internal class VUserProgressRepositoryFixture : IDisposable
    {
        public VUserProgressRepositoryFixture()
        {
            Context = CreateContext();
            Repository = new VUserProgressRepository(Context);
            InitDatabase();
        }

        public DIMSContext Context { get; }
        public IReadOnlyRepository<VUserProgress> Repository { get; }
        private void InitDatabase()
        {
            var entity = Context.VUserProgresses.Add(new VUserProgress()
            {
                Name = "Test",
                UserName = "Test",
                TrackDate = DateTime.Now,
                TrackNote = "Test"
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
