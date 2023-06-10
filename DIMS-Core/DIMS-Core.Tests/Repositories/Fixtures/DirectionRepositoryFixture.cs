using System;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Models;
using DIMS_Core.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DIMS_Core.Tests.Repositories.Fixtures
{
    internal sealed class DirectionRepositoryFixture : AbstractRepositoryFixture<Direction>, IDisposable
    {
        public DirectionRepositoryFixture() : base()
        {
            Repository = new DirectionRepository(Context);
            InitDatabase();
        }

        public int DirectionId { get; private set; }

        protected override void InitDatabase()
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
    }
}