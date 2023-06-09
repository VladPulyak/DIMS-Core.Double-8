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
    internal sealed class TaskStateRepositoryFixture : AbstractRepositoryFixture, IDisposable
    {
        public TaskStateRepositoryFixture()
        {
            Repository = new TaskStateRepository(Context);
            InitDatabase();
        }

        public IRepository<TaskState> Repository { get; }

        public int StateId { get; private set; }

        protected override void InitDatabase()
        {
            var entity = Context.TaskStates.Add(new TaskState()
            {
                StateName = "Test"
            });
            Context.SaveChanges();
            StateId = entity.Entity.StateId;
            entity.State = EntityState.Detached;
        }
    }
}
