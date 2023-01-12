using Entities;
using System.Data;

namespace Service
{
    public interface IUsersService
    {
        Task<object> GetById(string userName, string password);
    }
}