using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Service
{
    public interface IUsersService
    {
        Task<ActionResult<User>> GetById(string userName, string password);
    }
}