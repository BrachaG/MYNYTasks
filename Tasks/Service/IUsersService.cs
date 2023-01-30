using Entities;

namespace Service
{
    public interface IUsersService
    {
        Task<User> GetById(string userName, string password);
    }
}