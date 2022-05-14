﻿using System.ComponentModel.DataAnnotations;

namespace MeowLearn.Entities
{
    public class Content
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 10)]
        public string Title { get; set; }
        public string  Description { get; set; }
        public string VideoLink { get; set; }
        public CategoryItem CategoryItem { get; set; }
    }
}