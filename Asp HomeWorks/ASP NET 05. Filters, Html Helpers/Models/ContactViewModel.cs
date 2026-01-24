namespace HomeTask.Models;

using System.ComponentModel.DataAnnotations;

public class ContactViewModel
{
    [Required(ErrorMessage = "Введите ваше имя")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Введите почту")]
    [EmailAddress(ErrorMessage = "Некорректный формат почты")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Напишите сообщение")]
    [StringLength(1000, MinimumLength = 10)]
    public string Message { get; set; }
}