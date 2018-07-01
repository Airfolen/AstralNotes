using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralNotes.Database.Entities;
using AstralNotes.Domain.Users.Models;

namespace AstralNotes.Domain.Users
{
    public interface IUserService
    {
        Task<User> GetCurrentUserAsync();
    }
}