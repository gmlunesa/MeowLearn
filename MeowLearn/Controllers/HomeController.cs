using MeowLearn.Data;
using MeowLearn.Entities;
using MeowLearn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MeowLearn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(
            ILogger<HomeController> logger,
            ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager
        )
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<CategoryItemDetailsModel> categoryItemDetailsModels = null;
            IEnumerable<CategoryItemsGroupByCategoryModel> categoryItemsGroupByCategoryModels =
                null;

            CategoryDetailsModel categoryDetailsModel = new CategoryDetailsModel();

            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    categoryItemDetailsModels = await GetCategoryItemDetailsForUser(user.Id);
                    categoryItemsGroupByCategoryModels = GetCategoryItemsGroupByCategory(
                        categoryItemDetailsModels
                    );

                    categoryDetailsModel.CategoryItemsGroupByCategoryModels =
                        categoryItemsGroupByCategoryModels;
                }
            }
            else
            {
                var categories = await GetCategoriesWithContent();

                categoryDetailsModel.Categories = categories;
            }

            return View(categoryDetailsModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
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

        private async Task<IEnumerable<CategoryItemDetailsModel>> GetCategoryItemDetailsForUser(
            string userId
        )
        {
            return await (
                from categoryItem in _context.CategoryItem
                join category in _context.Category on categoryItem.CategoryId equals category.Id
                join content in _context.Content on categoryItem.Id equals content.CategoryItem.Id
                join userCategory in _context.UserCategory
                    on category.Id equals userCategory.CategoryId
                join mediaType in _context.MediaType on categoryItem.MediaTypeId equals mediaType.Id
                where userCategory.UserId == userId
                select new CategoryItemDetailsModel
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    CategoryItemId = categoryItem.Id,
                    CategoryItemName = categoryItem.Name,
                    CategoryItemDescription = categoryItem.Description,
                    MediaImagePath = mediaType.ThumbnailImagePath
                }
            ).ToListAsync();
        }

        private IEnumerable<CategoryItemsGroupByCategoryModel> GetCategoryItemsGroupByCategory(
            IEnumerable<CategoryItemDetailsModel> categoryItemDetails
        )
        {
            return (
                from item in categoryItemDetails
                group item by item.CategoryId into categoryGroup
                select new CategoryItemsGroupByCategoryModel
                {
                    Id = categoryGroup.Key,
                    Name = categoryGroup.Select(c => c.CategoryItemName).FirstOrDefault(),
                    Items = categoryGroup
                }
            );
        }
    }
}
