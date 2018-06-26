using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralNotes.Database;
using AstralNotes.Database.Entities;
using AstralNotes.Domain.Users.Models;
using AstralNotes.Utils.Password;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace AstralNotes.Domain.Users
{
    public class UserService : IUserService
    {
        private readonly NotesContext _context;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(NotesContext context, IPasswordValidator passwordValidator,
            IPasswordHasher passwordHasher, IMapper mapper)
        {
            _context = context;
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<Guid> Create(UserInfo model)
        {
            if (!_passwordValidator.Validate(model.Password))
                throw new InvalidOperationException("Некорректный  пароль");
            
            var user = _mapper.Map<UserInfo, User>(model);
            
            user.Salt = Randomizer.GetString(10);
            user.PasswordHash = _passwordHasher.Hash(model.Password + user.Salt);
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.UserGuid;
        }
  
        public async Task Remove(Guid userGuid)
        {
            var user = await _context.Users.FindAsync(userGuid);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel> GetUser(Guid userGuid)
        {
            var user = await _context.Users
                .ProjectTo<UserModel>().AsNoTracking().FirstOrDefaultAsync(u => u.UserGuid == userGuid);

            return user;
        }

        public async Task<List<UserModel>> GetUsers(string search)
        {
            var result = _context.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim().ToLower();
                result = result.Where(x => x.FullName.ToLower().Contains(search));
            }

            return await result.ProjectTo<UserModel>().ToListAsync();
        }
    }
}
