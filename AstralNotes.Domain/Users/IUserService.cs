using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralNotes.Domain.Users.Models;

namespace AstralNotes.Domain.Users
{
    public interface IUserService
    {
        Task<Guid> Create(UserInfo model);
        
        Task Remove(Guid userGuid);
        
        Task<UserModel> GetUser(Guid userGuid);

        Task<List<UserModel>> GetUsers(string search);
    }
}