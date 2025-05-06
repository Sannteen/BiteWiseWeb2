using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BiteWiseWeb2.Models;
    public class FoodLog
    {
        [Key]
        public int RecordId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        public string FoodName { get; set; }

        [Required]
        [Range(0, 5000)]
        public int CalsConsumed { get; set; }

        public virtual User User { get; set; }
    }
