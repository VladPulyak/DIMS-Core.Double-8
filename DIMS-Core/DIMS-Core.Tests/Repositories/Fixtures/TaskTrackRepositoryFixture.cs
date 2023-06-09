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
    internal sealed class TaskTrackRepositoryFixture : AbstractRepositoryFixture, IDisposable
    {
        public TaskTrackRepositoryFixture()
        {
            Repository = new TaskTrackRepository(Context);
            InitDatabase();
        }

        public IRepository<TaskTrack> Repository { get; }

        public int TaskTrackId { get; set; }
        
        protected override void InitDatabase()
        {
            var entity = Context.TaskTracks.Add(new TaskTrack
            {
                TrackNote = "Test",
                TrackDate = DateTime.Now
            });
            Context.SaveChanges();
            TaskTrackId = entity.Entity.TaskTrackId;
            entity.State = EntityState.Detached;
        }
    }
}
