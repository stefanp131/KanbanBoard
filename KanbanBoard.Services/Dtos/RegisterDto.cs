using System.ComponentModel.DataAnnotations;

namespace KanbanBoard.Services.Dtos;

public class RegisterDto
{
    [Required] public string Username { get; set; }

    [Required]
    [MinLength(4)]
    public string Password { get; set; }
}