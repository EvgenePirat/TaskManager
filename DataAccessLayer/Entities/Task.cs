using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Model class for hold information about task from user
    /// </summary>
    public class Task
    {
        public Guid Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public DateTime FinishTime { get; set; }

        [Required]
        public string? Status { get; set; }

        [Required]
        public DateTime Created { get; set; } = DateTime.Now;

        public Guid CategoryId { get; set; }
        
        public Category? Category { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public User? User { get; set; }
    }
}
