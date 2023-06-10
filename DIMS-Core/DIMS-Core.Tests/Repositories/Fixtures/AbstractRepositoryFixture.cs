using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.Tests.Repositories.Fixtures
{
    public abstract class AbstractRepositoryFixture<TEntity> : IDisposable
        where TEntity : class
    {
        public AbstractRepositoryFixture()
        {
            Context = CreateContext();
        }

        public IRepository<TEntity> Repository { get; protected set; }

        public DIMSContext Context { get; }

        protected abstract void InitDatabase();

        private static DIMSContext CreateContext()
        {
            var options = GetOptions();

            return new DIMSContext(options);
        }

        private static DbContextOptions<DIMSContext> GetOptions()
        {
            var builder = new DbContextOptionsBuilder<DIMSContext>().UseInMemoryDatabase(GetInMemoryDbName());

            return builder.Options;
        }

        private static string GetInMemoryDbName()
        {
            return $"InMemory_{Guid.NewGuid()}";
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
