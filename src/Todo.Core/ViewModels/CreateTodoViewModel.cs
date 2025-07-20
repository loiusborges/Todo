using System.ComponentModel.DataAnnotations;

namespace Todo.Core.ViewModels;

public class CreateTodoViewModel
{
    [Required]
    public string Title { get; set; }
}
