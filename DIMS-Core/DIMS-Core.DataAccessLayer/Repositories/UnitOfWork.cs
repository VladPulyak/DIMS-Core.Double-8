using System;
using AsyncTask = System.Threading.Tasks.Task;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Models;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    /// <summary>
    ///     This class is unit of work pattern.
    ///     He is pretty popular in projects which have repository approach and using when you need to have access to many
    ///     repositories in one class under one context.
    ///     You can read about the pattern in Internet.
    /// </summary>
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DIMSContext _context;

        public UnitOfWork(DIMSContext context,
                          IRepository<UserProfile> userProfileRepository,
                          IRepository<Direction> directionRepository,
                          IRepository<TaskState> taskStateRepository,
                          IRepository<TaskTrack> taskTrackRepository,
                          IRepository<UserTask> userTaskRepository,
                          IRepository<Task> taskRepository,
                          IReadOnlyRepository<VTask> vTaskRepository,
                          IReadOnlyRepository<VUserProgress> vUserProgressRepository,
                          IReadOnlyRepository<VUserTrack> vUserTrackRepository,
                          IReadOnlyRepository<VUserTask> vUserTaskRepository,
                          IReadOnlyRepository<VUserProfile> vUserProfileRepository)
        {
            _context = context;

            UserProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
            DirectionRepository = directionRepository ?? throw new ArgumentNullException(nameof(directionRepository));
            UserTaskRepository = userTaskRepository ?? throw new ArgumentNullException(nameof(userTaskRepository));
            TaskTrackRepository = taskTrackRepository ?? throw new ArgumentNullException(nameof(taskTrackRepository));
            TaskStateRepository = taskStateRepository ?? throw new ArgumentNullException(nameof(taskStateRepository));
            TaskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            VUserProfileRepository = vUserProfileRepository ?? throw new ArgumentNullException(nameof(vUserProfileRepository));
            VUserTaskRepository = vUserTaskRepository ?? throw new ArgumentNullException(nameof(vUserTaskRepository));
            VUserTrackRepository = vUserTrackRepository ?? throw new ArgumentNullException(nameof(vUserTrackRepository));
            VUserProgressRepository = vUserProgressRepository ?? throw new ArgumentNullException(nameof(vUserProgressRepository));
            VTaskRepository = vTaskRepository ?? throw new ArgumentNullException(nameof(vTaskRepository));
        }

        public IRepository<UserProfile> UserProfileRepository { get; }

        public IRepository<Direction> DirectionRepository { get; }

        public IRepository<TaskState> TaskStateRepository { get; }

        public IRepository<TaskTrack> TaskTrackRepository { get; }

        public IRepository<UserTask> UserTaskRepository { get; }

        public IRepository<Task> TaskRepository { get; }

        public IReadOnlyRepository<VUserProfile> VUserProfileRepository { get; }

        public IReadOnlyRepository<VUserTask> VUserTaskRepository { get; }

        public IReadOnlyRepository<VTask> VTaskRepository { get; }

        public IReadOnlyRepository<VUserProgress> VUserProgressRepository { get; }

        public IReadOnlyRepository<VUserTrack> VUserTrackRepository { get; }

        /// <summary>
        ///     This method is not important here because each repository already has same method.
        ///     But remember you can use repositories separately from unit of work. So 'Save' method exists in UnitOfWork and
        ///     Repository.
        /// </summary>
        /// <returns></returns>
        public AsyncTask Save()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}