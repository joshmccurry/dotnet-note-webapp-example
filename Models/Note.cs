using System;
using System.ComponentModel.DataAnnotations;

namespace dotnet_note_webapp_example.Models
{
    public class Note
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Content { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}