using System;
using AsyncTask =  System.Threading.Tasks.Task;
using DIMS_Core.DataAccessLayer.Models;
using DIMS_Core.Tests.Repositories.Fixtures;
using Microsoft.EntityFrameworkCore;
using Xunit;
using DIMS_Core.Common.Exceptions;

namespace DIMS_Core.Tests.Repositories
{
    public class DirectionRepositoryTests : IDisposable
    {
        public DirectionRepositoryTests()
        {
            Fixture = new DirectionRepositoryFixture();
        }

        private DirectionRepositoryFixture Fixture { get; }

        public void Dispose()
        {
            Fixture.Dispose();
        }

        [Fact]
        public async AsyncTask GetAll_OK()
        {
            // Act
            var result = await Fixture.Repository
                                       .GetAll()
                                       .ToArrayAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public async AsyncTask GetById_OK()
        {
            // Act
            var result = await Fixture.Repository.GetById(Fixture.DirectionId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Fixture.DirectionId, result.DirectionId);
            Assert.Equal("Test Direction", result.Name);
            Assert.Equal("Test Description", result.Description);
        }

        [Fact]
        public async AsyncTask GetById_EmptyId_Fail()
        {
            // Arrange
            const int id = 0;

            // Act, Assert
            await Assert.ThrowsAsync<InvalidArgumentException>(() => Fixture.Repository.GetById(id));
        }

        [Fact]
        public async AsyncTask GetById_NotExistDirection_Fail()
        {
            // Arrange
            const int id = int.MaxValue;

            // Act, Assert
            await Assert.ThrowsAsync<ObjectNotFoundException>(() => Fixture.Repository.GetById(id));
        }

        [Fact]
        public async AsyncTask Create_OK()
        {
            // Arrange
            var entity = new Direction
                         {
                             Name = "Create",
                             Description = "Description"
                         };

            // Act
            var result = await Fixture.Repository.Create(entity);
            await Fixture.Context.SaveChangesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(default, result.DirectionId);
            Assert.Equal(entity.Name, result.Name);
            Assert.Equal(entity.Description, result.Description);
        }

        [Fact]
        public async AsyncTask Create_EmptyEntity_Fail()
        {
            // Arrange Act, Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => Fixture.Repository.Create(null));
        }

        [Fact]
        public async AsyncTask Update_OK()
        {
            // Arrange
            var entity = new Direction
                         {
                             DirectionId = Fixture.DirectionId,
                             Name = "Create",
                             Description = "Description"
                         };

            // Act
            var result = Fixture.Repository.Update(entity);
            await Fixture.Context.SaveChangesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(default, result.DirectionId);
            Assert.Equal(entity.Name, result.Name);
            Assert.Equal(entity.Description, result.Description);
        }

        [Fact]
        public void Update_EmptyEntity_Fail()
        {
            // Arrange Act, Assert
            Assert.Throws<ArgumentNullException>(() => Fixture.Repository.Update(null));
        }

        [Fact]
        public async AsyncTask Delete_OK()
        {
            // Act
            await Fixture.Repository.Delete(Fixture.DirectionId);
            await Fixture.Context.SaveChangesAsync();

            // Assert
            var deletedEntity = await Fixture.Context.Directions.FindAsync(Fixture.DirectionId);
            Assert.Null(deletedEntity);
        }

        [Fact]
        public async AsyncTask Delete_EmptyId_Fail()
        {
            // Arrange
            const int id = 0;

            // Act, Assert
            await Assert.ThrowsAsync<InvalidArgumentException>(() => Fixture.Repository.Delete(id));
        }
    }
}