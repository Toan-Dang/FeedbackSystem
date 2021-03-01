using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FS.Areas.Admin.Models;
using FS.Data;

namespace FS.Areas.Admin.Controllers {

    [Area("Admin")]
    public class EnrollmentsController : Controller {
        private readonly AppDbContext _context;

        public EnrollmentsController(AppDbContext context) {
            _context = context;
        }

        // GET: Admin/Enrollments
        public async Task<IActionResult> Index() {
            var appDbContext = _context.Enrollment.Include(e => e.Class).Include(e => e.TrainerID);
            ViewData["ClassID"] = new SelectList(_context.Classes, "ClassID", "ClassName");
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Enrollments/Details/5
        public async Task<IActionResult> Details(int? classid, string traineeid) {
            if(classid == null || traineeid == null) {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Class)
                .Include(e => e.TrainerID)
                .FirstOrDefaultAsync(m => m.ClassID == classid);
            if(enrollment == null) {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Admin/Enrollments/Create
        public IActionResult Create() {
            ViewData["ClassID"] = new SelectList(_context.Classes, "ClassID", "ClassName");
            ViewData["TraineeID"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Admin/Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassID,TraineeID")] Enrollment enrollment) {
            if(ModelState.IsValid) {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassID"] = new SelectList(_context.Classes, "ClassID", "ClassName", enrollment.ClassID);
            ViewData["TraineeID"] = new SelectList(_context.Users, "Id", "UserName", enrollment.TraineeID);
            return View(enrollment);
        }

        // GET: Admin/Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? classid, string traineeid) {
            if(classid == null || traineeid == null) {
                return NotFound();
            }

            var enrollment = await _context.Enrollment.FindAsync(classid, traineeid);
            if(enrollment == null) {
                return NotFound();
            }
            ViewData["ClassID"] = new SelectList(_context.Classes, "ClassID", "ClassName", enrollment.ClassID);
            ViewData["TraineeID"] = new SelectList(_context.Users, "Id", "UserName", enrollment.TraineeID);
            return View(enrollment);
        }

        // POST: Admin/Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int classid, string traineeid, [Bind("ClassID,TraineeID")] Enrollment enrollment) {
            if(classid != enrollment.ClassID) {
                return NotFound();
            }
            if(traineeid != enrollment.TraineeID) {
                return NotFound();
            }
            ViewData["ClassID"] = new SelectList(_context.Classes, "ClassID", "ClassName", enrollment.ClassID);
            ViewData["TraineeID"] = new SelectList(_context.Users, "Id", "UserName", enrollment.TraineeID);
            if(ModelState.IsValid) {
                try {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                } catch(DbUpdateConcurrencyException) {
                    if(!EnrollmentExists(enrollment.ClassID, enrollment.TraineeID)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(enrollment);
        }

        // GET: Admin/Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? classid, string traineeid) {
            if(classid == null || traineeid == null) {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Class)
                .Include(e => e.TrainerID)
                .FirstOrDefaultAsync(m => m.ClassID == classid);
            if(enrollment == null) {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Admin/Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? classid, string traineeid) {
            var enrollment = await _context.Enrollment.FindAsync(classid, traineeid);
            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int? classid, string traineeid) {
            return _context.Enrollment.Any(e => e.ClassID == classid);
        }
    }
}