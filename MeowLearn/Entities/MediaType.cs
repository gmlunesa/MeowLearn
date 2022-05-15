using MeowLearn.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeowLearn.Entities
{
    public class MediaType : IPrimaryProperties
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 10)]
        public string Name { get; set; }

        [Required]
        public string ThumbnailImagePath { get; set; }

        [ForeignKey("MediaTypeId")]
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }
    }
}
