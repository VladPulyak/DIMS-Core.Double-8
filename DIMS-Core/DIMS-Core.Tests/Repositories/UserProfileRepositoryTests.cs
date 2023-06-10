using DIMS_Core.Common.Exceptions;
using DIMS_Core.Tests.Repositories.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsyncTask = System.Threading.Tasks.Task;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities.Encoders;
using static System.Formats.Asn1.AsnWriter;
using DIMS_Core.DataAccessLayer.Models;

namespace DIMS_Core.Tests.Repositories
{
    public class UserProfileRepositoryTests : IDisposable
    {
        public UserProfileRepositoryTests()
        {
            Fixture = new UserProfileRepositoryFixture();
        }

        private UserProfileRepositoryFixture Fixture { get; }

        [Fact]
        public async AsyncTask GetAll_OK()
        {
            //Act
            var userProfiles = await Fixture.Repository.GetAll().ToListAsync();

            //Assert
            Assert.NotEmpty(userProfiles);
            Assert.Single(userProfiles);
        }

        [Fact]
        public async AsyncTask GetById_OK()
        {
            //Act
            var userProfile = await Fixture.Repository.GetById(Fixture.UserId);

            //Assert
            Assert.NotNull(userProfile);
            Assert.Equal("Test", userProfile.FirstName);
            Assert.Equal("Test", userProfile.LastName);
            Assert.Equal("Test@test.com", userProfile.Email);
            Assert.Equal("Test street", userProfile.Address);
            Assert.Equal(DateTime.Now.ToString(), userProfile.BirthDate.ToString());
            Assert.Equal("QA", userProfile.Education);
            Assert.Equal(0.ToString(), userProfile.MathScore.ToString());
            Assert.Equal("1234567", userProfile.MobilePhone);
            Assert.Equal(0.ToString(), userProfile.Sex.ToString());
            Assert.Equal("Test", userProfile.Skype);
            Assert.Equal(DateTime.Now.ToString(), userProfile.StartDate.ToString());
            Assert.Equal(0.ToString(), userProfile.UniversityAverageScore.ToString());
        }

        [Fact]
        public async AsyncTask GetById_InvalidId_Fail()
        {
            //Arrange
            int id = 0;

            //Assert
            await Assert.ThrowsAsync<InvalidArgumentException>(() => Fixture.Repository.GetById(id));
        }

        [Fact]
        public async AsyncTask GetById_UserProfileNotFound_Fail()
        {
            //Arrange
            int id = int.MaxValue;

            //Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(() => Fixture.Repository.GetById(id));
        }

        [Fact]
        public async AsyncTask Create_OK()
        {
            //Arrange
            var userProfile = new UserProfile
            {
                FirstName = "Test2",
                LastName = "Test2",
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
            };

            //Act
            var createdUserProfile = await Fixture.Repository.Create(userProfile);

            //Assert
            Assert.NotNull(createdUserProfile);
            Assert.Equal(createdUserProfile.FirstName, userProfile.FirstName);
            Assert.Equal(createdUserProfile.LastName, userProfile.LastName);
            Assert.Equal(createdUserProfile.Email, userProfile.Email);
            Assert.Equal(createdUserProfile.Address, userProfile.Address);
            Assert.Equal(createdUserProfile.BirthDate.ToString(), userProfile.BirthDate.ToString());
            Assert.Equal(createdUserProfile.Education, userProfile.Education);
            Assert.Equal(createdUserProfile.MathScore.ToString(), userProfile.MathScore.ToString());
            Assert.Equal(createdUserProfile.MobilePhone, userProfile.MobilePhone);
            Assert.Equal(createdUserProfile.Sex.ToString(), userProfile.Sex.ToString());
            Assert.Equal(createdUserProfile.Skype, userProfile.Skype);
            Assert.Equal(createdUserProfile.StartDate.ToString(), userProfile.StartDate.ToString());
            Assert.Equal(createdUserProfile.UniversityAverageScore.ToString(), userProfile.UniversityAverageScore.ToString());
        }

        [Fact]
        public async AsyncTask Create_EmptyUserProfile_OK()
        {
            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => Fixture.Repository.Create(null));
        }

        [Fact]
        public async AsyncTask Update_OK()
        {
            //Arrange
            var userProfile = new UserProfile
            {
                UserId = Fixture.UserId,
                FirstName = "Test_Update",
                LastName = "Test_Update",
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
            };
            //Act
            var updatedUserProfile = Fixture.Repository.Update(userProfile);
            await Fixture.Repository.Save();

            //Assert
            Assert.NotNull(updatedUserProfile);
            Assert.Equal(updatedUserProfile.FirstName, userProfile.FirstName);
            Assert.Equal(updatedUserProfile.LastName, userProfile.LastName);
            Assert.Equal(updatedUserProfile.Email, userProfile.Email);
            Assert.Equal(updatedUserProfile.Address, userProfile.Address);
            Assert.Equal(updatedUserProfile.BirthDate.ToString(), userProfile.BirthDate.ToString());
            Assert.Equal(updatedUserProfile.Education, userProfile.Education);
            Assert.Equal(updatedUserProfile.MathScore.ToString(), userProfile.MathScore.ToString());
            Assert.Equal(updatedUserProfile.MobilePhone, userProfile.MobilePhone);
            Assert.Equal(updatedUserProfile.Sex.ToString(), userProfile.Sex.ToString());
            Assert.Equal(updatedUserProfile.Skype, userProfile.Skype);
            Assert.Equal(updatedUserProfile.StartDate.ToString(), userProfile.StartDate.ToString());
            Assert.Equal(updatedUserProfile.UniversityAverageScore.ToString(), userProfile.UniversityAverageScore.ToString());
        }

        [Fact]
        public void Update_EmptyUserProfile_Fail()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => Fixture.Repository.Update(null));
        }

        [Fact]
        public async AsyncTask Delete_OK()
        {
            //Act
            await Fixture.Repository.Delete(Fixture.UserId);
            await Fixture.Repository.Save();

            //Assert
            var deletedTask = await Fixture.Context.UserProfiles.FindAsync(Fixture.UserId);
            Assert.Null(deletedTask);
        }

        [Fact]
        public async AsyncTask Delete_EmptyId_Fail()
        {
            //Arrange
            int id = 0;

            //Assert
            await Assert.ThrowsAsync<InvalidArgumentException>(() => Fixture.Repository.Delete(id));
        }

        public void Dispose()
        {
            Fixture.Dispose();
        }
    }
}
