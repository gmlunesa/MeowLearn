using MeowLearn.Areas.Admin.Models;
using MeowLearn.Data;
using MeowLearn.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeowLearn.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Category.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCategoriesPerCategory(int categoryId)
        {
            UserCategoryListModel userCategoryListModel = new UserCategoryListModel();

            var allUsers = await GetAllUsersAsync();
            var usersPerCategory = await GetUsersPerCategoryAsync(categoryId);

            userCategoryListModel.Users = allUsers;
            userCategoryListModel.UsersSelected = usersPerCategory;

            return PartialView("_UserListPartial", userCategoryListModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSelectedUsers(
            [Bind("CategoryId, UsersSelected")] UserCategoryListModel userCategoryListModel
        )
        {
            List<UserCategory> userCategoriesToAdd = null;

            if (userCategoryListModel.UsersSelected != null)
            {
                userCategoriesToAdd = await GetUserCategoriesAsync(userCategoryListModel);
            }
            var userCategoriesToDelete = await GetUserCategoriesAsync(
                userCategoryListModel.CategoryId
            );

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

            userCategoryListModel.Users = await GetAllUsersAsync();

            return PartialView("_UserListPartial", userCategoryListModel);
        }

        /// <summary>
        /// Returns list of all users from the database in format of UserModel
        /// </summary>
        /// <returns></returns>
        private async Task<List<UserModel>> GetAllUsersAsync()
        {
            var allUsers = await (
                from user in _context.Users
                select new UserModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                }
            ).ToListAsync();

            return allUsers;
        }

        /// <summary>
        /// Returns list of all users per provided CategoryId in format of UserModel
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        private async Task<List<UserModel>> GetUsersPerCategoryAsync(int categoryId)
        {
            var savedUsersPerCategory = await (
                from userCategory in _context.UserCategory
                where userCategory.CategoryId == categoryId
                select new UserModel { Id = userCategory.UserId }
            ).ToListAsync();

            return savedUsersPerCategory;
        }

        /// <summary>
        /// Returns list of UserCategory mapped for each user selected
        /// </summary>
        /// <param name="userCategoryList"></param>
        /// <returns></returns>
        private async Task<List<UserCategory>> GetUserCategoriesAsync(
            UserCategoryListModel userCategoryList
        )
        {
            // Get the list of UserCategory mapped for each user selected
            var userCategories = (
                from userCategory in userCategoryList.UsersSelected
                select new UserCategory
                {
                    CategoryId = userCategoryList.CategoryId,
                    UserId = userCategory.Id,
                }
            ).ToList();

            return await Task.FromResult(userCategories);
        }

        /// <summary>
        /// Returns list of UserCategory mapped for the category specified
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        private async Task<List<UserCategory>> GetUserCategoriesAsync(int categoryId)
        {
            var userCategories = await (
                from userCategory in _context.UserCategory
                where userCategory.CategoryId == categoryId
                select new UserCategory
                {
                    Id = userCategory.Id,
                    CategoryId = categoryId,
                    UserId = userCategory.UserId
                }
            ).ToListAsync();

            return userCategories;
        }
    }
}
