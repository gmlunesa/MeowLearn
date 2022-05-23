using MeowLearn.Data.Interfaces;
using MeowLearn.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowLearn.Data
{
    public class DataFunctions : IDataFunctions
    {
        ApplicationDbContext _context;

        public DataFunctions(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task UpdateUserCategoryEntityAsync(
            List<UserCategory> userCategoriesToDelete,
            List<UserCategory> userCategoriesToAdd
        )
        {
            using (var dbContextTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.RemoveRange(userCategoriesToDelete);

                    await _context.SaveChangesAsync();

                    if (userCategoriesToAdd != null)
                    {
                        _context.AddRange(userCategoriesToAdd);
                        await _context.SaveChangesAsync();
                    }

                    await dbContextTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.DisposeAsync();
                }
            }
        }
    }
}
