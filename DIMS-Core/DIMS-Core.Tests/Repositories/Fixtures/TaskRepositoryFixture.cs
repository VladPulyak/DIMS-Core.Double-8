using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Models;
using DIMS_Core.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIMS_Core.Tests.Repositories.Fixtures
{
    internal sealed class TaskRepositoryFixture : AbstractRepositoryFixture<Task>, IDisposable
    {
        public TaskRepositoryFixture() : base()
        {
            Repository = new TaskRepository(Context);
            InitDatabase();
        }

        public int TaskId { get; private set; }

        protected override void InitDatabase()
        {
            var entity = Context.Tasks.Add(new Task()
            {
                Name = "Test",
                Description = "Test Task",
                StartDate = new DateTime(1900, 1, 1),
                DeadlineDate = DateTime.Now
            });
            TaskId = entity.Entity.TaskId;
            Context.SaveChanges();
            entity.State = EntityState.Detached;
        }
    }
}
