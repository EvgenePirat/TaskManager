using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ServiceContract
{
    /// <summary>
    /// Contract for remote validation any data in application
    /// </summary>
    public interface IRemoteValidationService
    {

        /// <summary>
        /// Method for check exist title for user
        /// </summary>
        /// <param name="titleTask">string title for check</param>
        /// <param name="loginUser">string login for find all tasks for user</param>
        /// <returns>returned true if not exist and false if exist</returns>
        Task<bool?> IsNameTaskAlreadyCreateAsync(string titleTask, string loginUser);
    }
}
