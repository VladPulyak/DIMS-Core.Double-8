using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface IUserTaskStoreProcedures
    {
        Task SetUserTaskAsSuccess(int userId, int taskId);
        Task SetUserTaskAsFail(int userId, int taskId);
    }
}
