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
    internal class VUserTrackRepositoryFixture : IDisposable
    {
        public VUserTrackRepositoryFixture()
        {
            Context = CreateContext();
            Repository = new VUserTrackRepository(Context);
            InitDatabase();
        }

        public DIMSContext Context { get; }
        public IReadOnlyRepository<VUserTrack> Repository { get; }
        private void InitDatabase()
        {
            var entity = Context.VUserTracks.Add(new VUserTrack()
            {
                TaskName = "Test",
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
