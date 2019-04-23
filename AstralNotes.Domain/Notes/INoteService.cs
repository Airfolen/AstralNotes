using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AstralNotes.Database.Enums;
using AstralNotes.Domain.Notes.Models;

namespace AstralNotes.Domain.Notes
{
    /// <summary>
    /// Интерфейс сервиса для работы с заметками
    /// </summary>
    public interface INoteService
    {
        /// <summary>
        /// Создание заметки
        /// <param name="model">Входная модель заметки</param>
        /// <param name="userId">Индетификатор пользователя</param>
        /// <returns>Индетификатор заметки</returns>
        /// </summary>
        Task<Guid> Create(NoteInfo model, string userId);
        
        /// <summary>
        /// Обновление заметки
        /// <param name="noteGuid">Индетификатор заметки</param>
        /// <param name="model">Входная модель заметки</param>
        /// <param name="userId">Индетификатор пользователя</param>
        /// <returns>Индетификатор заметки</returns>
        /// </summary>
        Task Update(Guid noteGuid, NoteInfo model, string userId);
        
        /// <summary>
        /// Удаление заметки
        /// <param name="noteGuid">Индетификатор заметки</param>
        /// </summary>
        Task Remove(Guid noteGuid);

        /// <summary>
        /// Удаление заметки c телефона
        /// <param name="noteGuid">Индетификатор заметки</param>
        /// </summary>
        Task<NoteModel> RemoveWithMobile(Guid noteGuid);
        
        /// <summary>
        /// Получение заметки для конкретного  пользователя
        /// <param name="noteGuid">Индетификатор заметки</param>
        /// <returns>Выходная модель заметки</returns>
        /// </summary>
        Task<NoteModel> GetNote(Guid noteGuid);
        
        /// <summary>
        /// Получение всех заметок для конкретного  пользователя
        /// <param name="search">Строка для поиска по содержимому заметки</param> 
        /// <param name="noteCategory">Категория заметки</param>
        /// <param name="userId">Индетификатор пользователя</param>
        /// <returns>Список выходных моделей заметок</returns>
        /// </summary>
        Task<List<NoteModel>> GetNotes(string search, NoteCategory? noteCategory, string userId);
    }
}