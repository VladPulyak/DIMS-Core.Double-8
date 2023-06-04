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
    internal class UserProfileRepositoryFixture : IDisposable
    {
        public UserProfileRepositoryFixture()
        {
            Context = CreateContext();
            Repository = new UserProfileRepository(Context);
            InitDatabase();
        }

        public DIMSContext Context { get; }
        public IRepository<UserProfile> Repository { get; }
        public int UserId { get; private set; }
        private void InitDatabase()
        {
            var entity = Context.UserProfiles.Add(new UserProfile
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@test.com",
                Address = "Test street",
                BirthDate = DateTime.Now,
                Education = "QA",
                MathScore = 0,
                MobilePhone = "1234567",
                Sex = 0,
                Skype = "Test",
                StartDate = DateTime.Now,
                UniversityAverageScore = 0
            });
            Context.SaveChanges();
            UserId = entity.Entity.UserId;
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