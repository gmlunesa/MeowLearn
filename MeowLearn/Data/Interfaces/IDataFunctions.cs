using MeowLearn.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowLearn.Data.Interfaces
{
    public interface IDataFunctions
    {
        Task UpdateUserCategoryEntityAsync(
            List<UserCategory> userCategoriesToDelete,
            List<UserCategory> userCategoriesToAdd
        );
    }
}
