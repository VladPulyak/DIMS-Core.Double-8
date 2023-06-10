using System;
using AsyncTask = System.Threading.Tasks.Task;
using DIMS_Core.DataAccessLayer.Models;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<UserProfile> UserProfileRepository { get; }

        IRepository<Direction> DirectionRepository { get; }

        IRepository<Task> TaskRepository { get; }

        IRepository<TaskState> TaskStateRepository { get; }

        IRepository<UserTask> UserTaskRepository { get; }

        IRepository<TaskTrack> TaskTrackRepository { get; }

        IReadOnlyRepository<VUserProfile> VUserProfileRepository { get; }

        IReadOnlyRepository<VTask> VTaskRepository { get; }

        IReadOnlyRepository<VUserProgress> VUserProgressRepository { get; }

        IReadOnlyRepository<VUserTask> VUserTaskRepository { get; }

        IReadOnlyRepository<VUserTrack> VUserTrackRepository { get; }

        AsyncTask Save();
    }
}