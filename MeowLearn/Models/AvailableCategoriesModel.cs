using MeowLearn.Entities;
using System.Collections.Generic;

namespace MeowLearn.Models
{
    public class AvailableCategoriesModel
    {
        public string UserId { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Category> CategoriesSelected { get; set; }
    }
}
