using BusinessLayer.Models.Roles.Response;

namespace BusinessLayer.ServiceContract
{
    /// <summary>
    /// Rerpesents business logic for manipoulating role entity
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Method for get all roles
        /// </summary>
        /// <returns>return a list of objects of roleresponse type</returns>
        public Task<List<RoleModel>> GetAllRolesAsync();
    }
}
