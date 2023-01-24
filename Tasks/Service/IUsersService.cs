using Entities;
using System.Data;

namespace Service
{
    public interface IUsersService
    {
        Task<User> GetById(string userName, string password);
    }
}