using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeowLearn.Data;
using MeowLearn.Entities;

namespace MeowLearn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Content/Create
        public IActionResult Create(int categoryItemId, int categoryId)
        {
            // Create Content objects with the parameters received from CategoryItem.Index view
            Content content = new Content { CatItemId = categoryItemId, CategoryId = categoryId, };
            // Pass parameters to the Create view
            return View(content);
        }

        // POST: Admin/Content/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,Description,VideoLink,CatItemId,CategoryId")] Content content
        )
        {
            if (ModelState.IsValid)
            {
                // Assign relevant CategoryItem using the CatItemId
                content.CategoryItem = await _context.CategoryItem.FindAsync(content.CatItemId);
                _context.Add(content);
                await _context.SaveChangesAsync();
                return RedirectToAction(
                    nameof(Index),
                    "CategoryItem",
                    new { categoryId = content.CategoryId }
                );
            }
            return View(content);
        }

        // GET: Admin/Content/Edit/5
        public async Task<IActionResult> Edit(int categoryItemId, int categoryId)
        {
            if (categoryItemId == 0)
            {
                return NotFound();
            }

            // Retrieve Content object from DbContext using parameters parameters received from CategoryItem.Index view
            var content = await _context.Content.SingleOrDefaultAsync(
                item => item.CategoryItem.Id == categoryItemId
            );

            content.CategoryId = categoryId;

            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }

        // POST: Admin/Content/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Title,Description,VideoLink,CategoryId")] Content content
        )
        {
            if (id != content.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(content);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentExists(content.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Use CategoryId parameter
                return RedirectToAction(
                    nameof(Index),
                    "CategoryItem",
                    new { categoryId = content.CategoryId }
                );
            }
            return View(content);
        }

        private bool ContentExists(int id)
        {
            return _context.Content.Any(e => e.Id == id);
        }
    }
}
