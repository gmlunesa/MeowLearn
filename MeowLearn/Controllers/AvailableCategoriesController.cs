using MeowLearn.Data;
using MeowLearn.Data.Interfaces;
using MeowLearn.Entities;
using MeowLearn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeowLearn.Controllers
{
    [Authorize]
    public class AvailableCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDataFunctions _dataFunctions;

        public AvailableCategoriesController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IDataFunctions dataFunctions
        )
        {
            _context = context;
            _userManager = userManager;
            _dataFunctions = dataFunctions;
        }

        public async Task<IActionResult> Index()
        {
            AvailableCategoriesModel availableCategoriesModel = new AvailableCategoriesModel();

            var userId = _userManager.GetUserAsync(User).Result?.Id;

            availableCategoriesModel.Categories = await GetCategoriesWithContent();
            availableCategoriesModel.CategoriesSelected = await GetCategoriesForUser(userId);

            availableCategoriesModel.UserId = userId;

            return View(availableCategoriesModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string[] categoriesSelected)
        {
            var userId = _userManager.GetUserAsync(User).Result?.Id;

            List<UserCategory> userCategoriesToDelete = await GetCategoriesToDeleteForUser(userId);
            List<UserCategory> userCategoriesToAdd = GetCategoriesToAddForUser(
                categoriesSelected,
                userId
            );

            await _dataFunctions.UpdateUserCategoryEntityAsync(
                userCategoriesToDelete,
                userCategoriesToAdd
            );

            return RedirectToAction("Index", "Home");
        }

        private async Task<List<Category>> GetCategoriesWithContent()
        {
            var categories = await (
                from category in _context.Category
                join categoryItem in _context.CategoryItem
                    on category.Id equals categoryItem.CategoryId
                join content in _context.Content on categoryItem.Id equals content.CategoryItem.Id
                select new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ThumbnailImagePath = category.ThumbnailImagePath
                }
            ).Distinct().ToListAsync();

            return categories;
        }

        private async Task<List<Category>> GetCategoriesForUser(string userId)
        {
            var categories = await (
                from userCategory in _context.UserCategory
                where userCategory.UserId == userId
                select new Category { Id = userCategory.CategoryId, }
            ).ToListAsync();

            return categories;
        }

        private async Task<List<UserCategory>> GetCategoriesToDeleteForUser(string userId)
        {
            var categories = await (
                from userCategory in _context.UserCategory
                where userCategory.UserId == userId
                select new UserCategory
                {
                    Id = userCategory.Id,
                    CategoryId = userCategory.CategoryId,
                    UserId = userId
                }
            ).ToListAsync();

            return categories;
        }

        private List<UserCategory> GetCategoriesToAddForUser(
            string[] categoriesSelected,
            string userId
        )
        {
            var categories = (
                from categoryId in categoriesSelected
                select new UserCategory { UserId = userId, CategoryId = int.Parse(categoryId), }
            ).ToList();

            return categories;
        }
    }
}
