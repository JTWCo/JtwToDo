using System;
using System.Collections.Generic;

namespace JTWToDo.Data.Models
{
    public partial class ToDo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? Completed { get; set; }
        public DateTime LastUpdated { get; set; }
        public byte[] ConcurrencyVersion { get; set; }
    }
}
