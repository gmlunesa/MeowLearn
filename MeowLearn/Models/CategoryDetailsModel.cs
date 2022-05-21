using MeowLearn.Entities;
using System.Collections.Generic;

namespace MeowLearn.Models
{
    public class CategoryDetailsModel
    {
        public IEnumerable<CategoryItemsGroupByCategoryModel> CategoryItemsGroupByCategoryModels { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
