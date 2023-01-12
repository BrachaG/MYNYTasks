using Entities;

namespace Repository
{
    public class UsersRepository : IUsersRepository
    {
        List<User> Users = new List<User>() {
            new User(){ iPersonId=1,
                         nvUserName= "ayala",
                         nvPasswordperty ="123",
                         nvPasswordHint= "123",
                         iLastUpdateUserId=1,
                         dtLastUpdateDate=new DateTime(),
                         iSysRowStatus =1,
                         iPersonStatusId=1,
                         iAdvancedOptionsId =1,
                        iValidUserNameStatus =1
            }
    };




        public async Task<User> GetById(string userName, string password)
        {

            foreach (var user in Users)
            {
                if (user.nvUserName == userName && user.nvPasswordperty == password)
                    return await Task.FromResult(user);
            }
            return null;
        }

    }

}