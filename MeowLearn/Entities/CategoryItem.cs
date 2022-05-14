using System;
using System.ComponentModel.DataAnnotations;

namespace MeowLearn.Entities
{
    public class CategoryItem
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 10)]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int MediaTypeId { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
