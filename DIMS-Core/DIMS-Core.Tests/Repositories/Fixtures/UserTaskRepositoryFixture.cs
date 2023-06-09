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
    internal sealed class UserTaskRepositoryFixture : AbstractRepositoryFixture, IDisposable
    {
        public UserTaskRepositoryFixture()
        {
            Repository = new UserTaskRepository(Context);
            InitDatabase();
        }

        public IRepository<UserTask> Repository { get; }

        public int UserTaskId { get; private set; }

        protected override void InitDatabase()
        {
            var entity = Context.UserTasks.Add(new UserTask());
            Context.SaveChanges();
            UserTaskId = entity.Entity.UserTaskId;
            entity.State = EntityState.Detached;
        }
    }
}
