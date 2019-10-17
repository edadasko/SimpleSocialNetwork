using System;
using System.ComponentModel.DataAnnotations;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork.ViewModels
{
    public class RegisterViewModel
    {
        private const string errorMessage = "Данное поле обязательно";

        [Required(ErrorMessage = errorMessage)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = errorMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = errorMessage)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = errorMessage)]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = errorMessage)]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = errorMessage)]
        [Display(Name = "Пол")]
        public Gender Gender { get; set; }

    }
}
