/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FS.Areas.Admin.Models;
using FS.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity;
using FS.Models;

namespace tesm {
    public class ModulesController : Controller {
        private readonly ILogger<ModulesController> _logger;
        private readonly UserManager<AppUser> _usermanager;
        private readonly AppDbContext _context;

        // Số bài hiện thị viết trên một trang danh mục
        public const int ITEMS_PER_PAGE = 10;

        public ModulesController(AppDbContext context, ILogger<ModulesController> logger, UserManager<AppUser> usermanager) {
            _context = context;
            _usermanager = usermanager;
            _logger = logger;
        }

        // GET: Admin/Modules
        public async Task<IActionResult> Index([Bind(Prefix = "page")] int pageNumber) {
            if(pageNumber == 0)
                pageNumber = 1;
            var listmodule = _context.Module;
            return View(await _context.Module.ToListAsync());
        }

        // GET: Admin/Modules/Details/5
        public async Task<IActionResult> Details(int? id) {
            if(id == null) {
                return NotFound();
            }

            var @module = await _context.Module
                .FirstOrDefaultAsync(m => m.ModuleID == id);
            if(@module == null) {
                return NotFound();
            }

            return View(@module);
        }

        // GET: Admin/Modules/Create
        [BindProperty]
        public int[] selectedCategories { set; get; }

        [Area("Admin")]
        public async Task<IActionResult> Create() {
            // Thông tin về User tạo Post
            var user = await _usermanager.GetUserAsync(User);
            ViewData["userpost"] = $"{user.UserName} {user.FullName}";
            // Danh mục chọn để đăng bài Post, tạo MultiSelectList
            //var feedbacks = await _context.Feedbacks.ToListAsync();
            var feedback = await _context.Feedbacks.ToListAsync();
            ViewData["feedbacks"] = new MultiSelectList(feedback, "FeedbackId", "Title");
            return View();
        }

        // POST: Admin/Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Admin")]
        public async Task<IActionResult> Create([Bind("ModuleID,AdminID,ModuleName,StartTime,EndTime,IsDeleted,FeedbackStartTime,FeedbackEndTime,FeedbackID")] Module @module) {
            var user = await _usermanager.GetUserAsync(User);
            ViewData["userpost"] = $"{user.UserName} {user.FullName}";

            if(ModelState.IsValid) {
                if(module.AdminID == null) {
                    module.AdminID = user.Id;
                }
                _context.Add(module);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@module);
        }

        // GET: Admin/Modules/Edit/5
        [Area("Admin")]
        public async Task<IActionResult> Edit(int? id) {
            if(id == null) {
                return NotFound();
            }

            var @module = await _context.Module.FindAsync(id);
            if(@module == null) {
                return NotFound();
            }
            return View(@module);
        }

        // POST: Admin/Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ModuleID,AdminID,ModuleName,StartTime,EndTime,IsDeleted,FeedbackStartTime,FeedbackEndTime,FeedbackID")] Module @module) {
            if(id != @module.ModuleID) {
                return NotFound();
            }

            if(ModelState.IsValid) {
                try {
                    _context.Update(@module);
                    await _context.SaveChangesAsync();
                } catch(DbUpdateConcurrencyException) {
                    if(!ModuleExists(@module.ModuleID)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@module);
        }

        // GET: Admin/Modules/Delete/5
        [Area("Admin")]
        public async Task<IActionResult> Delete(int? id) {
            if(id == null) {
                return NotFound();
            }

            var @module = await _context.Module
                .FirstOrDefaultAsync(m => m.ModuleID == id);
            if(@module == null) {
                return NotFound();
            }

            return View(@module);
        }

        // POST: Admin/Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Area("Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var @module = await _context.Module.FindAsync(id);
            _context.Module.Remove(@module);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(int id) {
            return _context.Module.Any(e => e.ModuleID == id);
        }
    }
}
*/