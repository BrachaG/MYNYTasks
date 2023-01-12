using Entities;

namespace Repository
{
    public interface IUsersRepository
    {
        Task<User> GetById(string userName, string password);
    }
}