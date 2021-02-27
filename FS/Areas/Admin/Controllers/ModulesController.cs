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
using Microsoft.AspNetCore.Identity;
using FS.Models;

namespace FS.Areas.Admin.Controllers {

    [Area("Admin")]
    public class ModulesController : Controller {
        private readonly AppDbContext _context;
        private readonly ILogger<ModulesController> _logger;
        private readonly UserManager<AppUser> _usermanager;

        public ModulesController(AppDbContext context,
            ILogger<ModulesController> logger,
             UserManager<AppUser> usermanager) {
            _context = context;
            _logger = logger;
            _usermanager = usermanager;
        }

        public const int ITEMS_PER_PAGE = 4;

        // GET: Admin/Modules
        public async Task<IActionResult> Index([Bind(Prefix = "page")] int pageNumber) {
            if(pageNumber == 0)
                pageNumber = 1;
            var appDbContext = _context.Modules
                .Include(p => p.AdminID)
                .Include(c => c.Feedbacks)
                .OrderByDescending(p => p.StartTime);

            _logger.LogInformation(pageNumber.ToString());
            // Lấy tổng số dòng dữ liệu
            var totalItems = appDbContext.Count();
            // Tính số trang hiện thị (mỗi trang hiện thị ITEMS_PER_PAGE mục)
            int totalPages = (int)Math.Ceiling((double)totalItems / ITEMS_PER_PAGE);

            if(pageNumber > totalPages)
                return RedirectToAction(nameof(ModulesController.Index), new { page = totalPages });

            var posts = await appDbContext
                .Skip(ITEMS_PER_PAGE * (pageNumber - 1))
                .Take(ITEMS_PER_PAGE)
                .ToListAsync();

            ViewData["pageNumber"] = pageNumber;
            ViewData["totalPages"] = totalPages;

            return View(posts.AsEnumerable());
        }

        // GET: Admin/Modules/Details/5
        public async Task<IActionResult> Details(int? id) {
            if(id == null) {
                return NotFound();
            }

            var @module = await _context.Modules
                .Include(p => p.AdminID)
                .Include(p => p.Feedbacks)
                .FirstOrDefaultAsync(m => m.ModuleID == id);
            if(@module == null) {
                return NotFound();
            }

            return View(@module);
        }

        // GET: Admin/Modules/Create
        public IActionResult Create() {
            // chon danh muc module

            ViewData["AdminId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["FeedbackID"] = new SelectList(_context.Feedbacks, "FeedbackId", "Title");
            return View();
        }

        // POST: Admin/Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleID,AdminId,ModuleName,StartTime,EndTime,IsDeleted,FeedbackStartTime,FeedbackEndTime,FeedbackID")] Module @module) {
            if(ModelState.IsValid) {
                _context.Add(@module);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Users, "Id", "UserName", @module.AdminId);
            ViewData["FeedbackID"] = new SelectList(_context.Feedbacks, "FeedbackId", "Title", @module.FeedbackID);
            return View(@module);
        }

        // GET: Admin/Modules/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if(id == null) {
                return NotFound();
            }

            var @module = await _context.Modules.FindAsync(id);
            if(@module == null) {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Users, "Id", "UserName", @module.AdminId);
            ViewData["FeedbackID"] = new SelectList(_context.Feedbacks, "FeedbackId", "Title", @module.FeedbackID);
            return View(@module);
        }

        // POST: Admin/Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModuleID,AdminId,ModuleName,StartTime,EndTime,IsDeleted,FeedbackStartTime,FeedbackEndTime,FeedbackID")] Module @module) {
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
            ViewData["AdminId"] = new SelectList(_context.Users, "Id", "UserName", @module.AdminId);
            ViewData["FeedbackID"] = new SelectList(_context.Feedbacks, "FeedbackId", "Title", @module.FeedbackID);
            return View(@module);
        }

        // GET: Admin/Modules/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if(id == null) {
                return NotFound();
            }

            var @module = await _context.Modules
               .Include(p => p.AdminID)
               .Include(p => p.Feedbacks)
                .FirstOrDefaultAsync(m => m.ModuleID == id);
            if(@module == null) {
                return NotFound();
            }

            return View(@module);
        }

        // POST: Admin/Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var @module = await _context.Modules.FindAsync(id);
            _context.Modules.Remove(@module);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(int id) {
            return _context.Modules.Any(e => e.ModuleID == id);
        }
    }
}