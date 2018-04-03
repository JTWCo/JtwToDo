using System;

namespace JTWToDo.Data
{
    public class ToDo : BaseEntity
    {
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? Completed { get; set; }
    }
}