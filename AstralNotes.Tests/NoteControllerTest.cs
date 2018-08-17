using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AstralNotes.API.Controllers;
using AstralNotes.Domain.Notes.Models;
using AstralNotes.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AstralNotes.Tests
{
    public class NoteControllerTest
    {
        readonly NoteController _noteController;

        public NoteControllerTest()
        {
            _noteController = new  NoteController(MockNoteService.GetMock(), MockUserService.GetMock());
        }

        [Fact]
        public void CanReturnsAViewResultAtCreating()
        {
            //Arrange && Act
            var result = _noteController.Create();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void CanGiveExceptionIfModelIsNotValidAtCreating()
        {
            //Arrange && Act
            var note  = new NoteInfo{Title = "SomeTitle"};

            //Assert
            Assert.Throws<ArgumentNullException>(() => SimulateValidation(null));
        }
        
        [Fact]
        public async void CanRedirectToHomeIndexIfModelValidAtCreating()
        {
            //Arrange
            var note  = new NoteInfo{Title = "SomeTitle", Content = "SomeContent"};
            
            //Act
            SimulateValidation(note);
            var result = await _noteController.Create(note);

            //Assert
            Assert.NotNull(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
        }
        
        [Fact]
        public async void CanRedirectToHomeIndexIfModelValidAtRemoving()
        {
            //Arrange
            var noteGuid  = new Guid("05a3537a-a12f-4d65-976e-1ff1fdb727c2");
            
            //Act
            var result = await _noteController.Remove(noteGuid);

            //Assert
            Assert.NotNull(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
        }
        
        [Fact]
        public async void CanRedirectToHomeIndexIfModelValidAtGetting()
        {
            //Arrange
            var noteGuid  = new Guid("05a3537a-a12f-4d65-976e-1ff1fdb727c2");
            
            //Act
            var result = await _noteController.Get(noteGuid);

            //Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<NoteModel>(
                viewResult.ViewData.Model);
        }
        
     
        /// <summary>
        /// Имитация поведение model binder, ответственного за проверку модели
        /// </summary>
        private void SimulateValidation(object model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            
            foreach (var validationResult in validationResults)
            {
                _noteController.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);
            }
        }
    }
}