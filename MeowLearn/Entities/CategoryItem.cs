using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeowLearn.Entities
{
    public class CategoryItem
    {
        private DateTime _defaultReleaseDate = DateTime.MinValue;
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 10)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Media Type")]
        public int MediaTypeId { get; set; }

        [NotMapped]
        public virtual ICollection<SelectListItem> MediaTypes { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate
        {
            get
            {
                return (_defaultReleaseDate == DateTime.MinValue)
                  ? DateTime.Now
                  : _defaultReleaseDate;
            }
            set { _defaultReleaseDate = value; }
        }

        [NotMapped]
        public int ContentId { get; set; }
    }
}
