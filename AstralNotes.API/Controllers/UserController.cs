using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralNotes.Domain.Users;
using AstralNotes.Domain.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AstralNotes.API.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// API для работы с пользователями
    /// </summary>
    [Route("Users")]
    public class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="model">Входная модель пользователя</param>
        [HttpPost]
     //   [Authorize(Roles="Администратор")]
        public async Task<Guid> Create([FromBody]UserInfo model)
        {
            return await _userService.Create(model);
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="userGuid">Идентификатор пользователя</param>
        [HttpDelete("{userGuid}")]
     //   [Authorize(Roles="Администратор")]
        public async Task Delete(Guid userGuid)
        {
            await _userService.Remove(userGuid);
        }
    
        /// <summary>
        /// Получение пользователя
        /// </summary>
        /// <param name="userGuid">Идентификатор пользователя</param>
        /// <returns>Выходная модель пользователя</returns>
        [HttpGet("{userGuid}")]
      //  [Authorize(Roles="Администратор")]
        public async Task<UserModel> GetUser(Guid userGuid)
        {
            return await _userService.GetUser(userGuid);
        }

        /// <summary>
        /// Получение пользователей
        /// </summary>
        /// <param name="search">Строка поиска по полям : FullName</param>
        /// <returns>Коллекция объектов? отсортированная по полю : FullName</returns>
        [HttpGet]
        //[Authorize(Roles="Администратор")]
        public async Task<List<UserModel>> GetUsers([FromQuery] string search)
        {
            var result = _userService.GetUsers(search);
            
            return  result.Result.OrderBy(x => x.FullName).ToList();;
        }
    }
}