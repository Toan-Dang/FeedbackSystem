using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FS.Areas.Admin.Models;
using FS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace FS.Areas.Admin.Controllers {

    [Area("Admin")]
    public class ClassesController : Controller {
        private readonly AppDbContext _context;
        private readonly ILogger<ClassesController> _logger;

        public ClassesController(AppDbContext context,
             ILogger<ClassesController> logger) {
            _context = context;
            _logger = logger;
        }

        // GET: Admin/Classes
        public const int ITEMS_PER_PAGE = 10;

        public async Task<IActionResult> Index([Bind(Prefix = "page")] int pageNumber) {
            if(pageNumber == 0)
                pageNumber = 1;
            var listclass = _context.Class;

            _logger.LogInformation(pageNumber.ToString());

            // Lấy tổng số dòng dữ liệu
            var totalItems = listclass.Count();
            // Tính số trang hiện thị (mỗi trang hiện thị ITEMS_PER_PAGE mục)
            int totalPages = (int)Math.Ceiling((double)totalItems / ITEMS_PER_PAGE);

            if(pageNumber > totalPages)
                return RedirectToAction(nameof(ClassesController.Index), new { page = totalPages });

            var posts = await listclass
                .Skip(ITEMS_PER_PAGE * (pageNumber - 1))
                .Take(ITEMS_PER_PAGE)
                .ToListAsync();

            ViewData["pageNumber"] = pageNumber;
            ViewData["totalPages"] = totalPages;

            return View(posts.AsEnumerable());
        }

        // GET: Admin/Classes/Details/5
        public async Task<IActionResult> Details(int? id) {
            if(id == null) {
                return NotFound();
            }

            var @class = await _context.Class
                .FirstOrDefaultAsync(m => m.ClassID == id);
            if(@class == null) {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Admin/Classes/Create
        [Authorize("Admin")]
        public IActionResult Create() {
            return View();
        }

        // POST: Admin/Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Admin")]
        public async Task<IActionResult> Create([Bind("ClassID,ClassName,Capacity,StartTime,EndTime,IsDeleted")] Class @class) {
            if(ModelState.IsValid) {
                _context.Add(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@class);
        }

        // GET: Admin/Classes/Edit/5
        [Authorize("Admin")]
        public async Task<IActionResult> Edit(int? id) {
            if(id == null) {
                return NotFound();
            }

            var @class = await _context.Class.FindAsync(id);
            if(@class == null) {
                return NotFound();
            }
            return View(@class);
        }

        // POST: Admin/Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ClassID,ClassName,Capacity,StartTime,EndTime,IsDeleted")] Class @class) {
            if(id != @class.ClassID) {
                return NotFound();
            }

            if(ModelState.IsValid) {
                try {
                    _context.Update(@class);
                    await _context.SaveChangesAsync();
                } catch(DbUpdateConcurrencyException) {
                    if(!ClassExists(@class.ClassID)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@class);
        }

        // GET: Admin/Classes/Delete/5
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int? id) {
            if(id == null) {
                return NotFound();
            }

            var @class = await _context.Class
                .FirstOrDefaultAsync(m => m.ClassID == id);
            if(@class == null) {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Admin/Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var @class = await _context.Class.FindAsync(id);
            _context.Class.Remove(@class);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id) {
            return _context.Class.Any(e => e.ClassID == id);
        }
    }
}