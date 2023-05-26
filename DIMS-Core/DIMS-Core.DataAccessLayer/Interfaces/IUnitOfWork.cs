using System;
using Task = System.Threading.Tasks.Task;
using DIMS_Core.DataAccessLayer.Models;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<UserProfile> UserProfileRepository { get; }

        IRepository<Direction> DirectionRepository { get; }

        IReadOnlyRepository<VUserProfile> VUserProfileRepository { get; }

        Task Save();
    }
}