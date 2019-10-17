using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleSocialNetwork.ViewModels
{
    public class LoginViewModel
    {
        private const string errorMessage = "Данное поле обязательно";

        [Required(ErrorMessage = errorMessage)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = errorMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
