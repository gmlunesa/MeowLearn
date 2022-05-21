using System.Linq;

namespace MeowLearn.Models
{
    public class CategoryItemsGroupByCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IGrouping<int, CategoryItemDetailsModel> Items { get; set; }
    }
}
