using System.ComponentModel.DataAnnotations;

namespace Todo.Core.ViewModels;

public class UptadeTodoViewModel
{
    [Required]
    public string Title { get; set; }
}