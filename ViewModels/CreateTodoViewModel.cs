using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public class CreateTodoViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}