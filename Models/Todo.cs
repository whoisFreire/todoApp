using System;

namespace TodoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int UserForeignKey { get; set; }
        public User User { get; set; }

    }
}