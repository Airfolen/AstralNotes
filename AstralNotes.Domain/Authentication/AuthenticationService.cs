//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using AstralNotes.Database;
//using AstralNotes.Database.Entities;
//using AstralNotes.Domain.Authentication.Models;
//using AstralNotes.Domain.Users.Models;
//using AstralNotes.Utils.Password;
//using AstralNotes.Utils.Tokens;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//
//namespace AstralNotes.Domain.Authentication
//{
//    public class AuthenticationService : IAuthenticationService
//    {
//        private readonly NotesContext _context;
//        private readonly IPasswordHasher _passwordHasher;
//        private readonly IMapper _mapper;
//
//        public AuthenticationService(NotesContext context, IPasswordHasher passwordHasher, IMapper mapper)
//        {
//            _context = context;
//            _passwordHasher = passwordHasher;
//            _mapper = mapper;
//        }
//
//        public async Task<AuthenticationResult> AuthenticateUser(SignInModel model)
//        {
//            var user = await _context.Users
//                .FirstOrDefaultAsync(u => u.Login == model.Login);
//
//            if (user == null || !_passwordHasher.VerifyHashedPassword(user.PasswordHash, _passwordHasher.Hash(model.Password + user.Salt)))
//                throw new InvalidOperationException("Неправильный логин или пароль");
//
//            var identity = GetIdentity(user);
//
//            var userModel = _mapper.Map<User, UserModel>(user);
//           
//            return new AuthenticationResult
//            {
//                Token = _tokenProvider.CreateToken(identity),
//                User = userModel
//            };
//        }
//
//        private static ClaimsIdentity GetIdentity(User user)
//        {
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
//            };
//
//            return
//                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
//        }
//    }
//}
