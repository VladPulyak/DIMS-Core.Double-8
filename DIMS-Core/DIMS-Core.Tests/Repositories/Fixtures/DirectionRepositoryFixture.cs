using System;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Models;
using DIMS_Core.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DIMS_Core.Tests.Repositories.Fixtures
{
    internal class DirectionRepositoryFixture : IDisposable
    {
        public DirectionRepositoryFixture()
        {
            Context = CreateContext();
            Repository = new DirectionRepository(Context);

            InitDatabase();
        }

        public DIMSContext Context { get; }

        public IRepository<Direction> Repository { get; }

        public int DirectionId { get; private set; }

        public void Dispose()
        {
            Context.Dispose();
        }

        private void InitDatabase()
        {
            var entry = Context.Directions.Add(new Direction
                                               {
                                                   Name = "Test Direction",
                                                   Description = "Test Description"
                                               });
            DirectionId = entry.Entity.DirectionId;

            Context.SaveChanges();
            entry.State = EntityState.Detached;
        }

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
    }
}