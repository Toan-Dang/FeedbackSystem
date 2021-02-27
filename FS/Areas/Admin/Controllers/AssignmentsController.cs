using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FS.Areas.Admin.Models;
using FS.Data;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FS.Areas.Admin.Controllers {

    [Area("Admin")]
    public class AssignmentsController : Controller {
        private readonly AppDbContext _context;

        public AssignmentsController(AppDbContext context) {
            _context = context;
        }

        // GET: Admin/Assignments
        public async Task<IActionResult> Index() {
            var appDbContext = _context.Assignment.Include(a => a.Admin).Include(a => a.Class).Include(a => a.Module);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Assignments/Details/5
        public async Task<IActionResult> Details(int? id) {
            if(id == null) {
                return NotFound();
            }

            var assignment = await _context.Assignment
                .Include(a => a.Admin)
                .Include(a => a.Class)
                .Include(a => a.Module)
                .FirstOrDefaultAsync(m => m.ClassID == id);
            if(assignment == null) {
                return NotFound();
            }

            return View(assignment);
        }

        // GET: Admin/Assignments/Create
        public IActionResult Create() {
            ViewData["TrainerID"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["ClassID"] = new SelectList(_context.Classes, "ClassID", "ClassName");
            ViewData["ModuleID"] = new SelectList(_context.Modules, "ModuleID", "ModuleName");
            return View();
        }

        // POST: Admin/Assignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassID,ModuleID,TrainerID,RegistrationCode")] Assignment assignment) {
            if(ModelState.IsValid) {
                var newassignment = new Assignment() {
                    ClassID = assignment.ClassID,
                    ModuleID = assignment.ModuleID,
                    TrainerID = assignment.TrainerID,

                    RegistrationCode = "CL" + assignment.ClassID.ToString() +
                    "M" + assignment.ModuleID.ToString() + "T" + assignment.TrainerID.ToString()
                };

                _context.Add(newassignment);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainerID"] = new SelectList(_context.Users, "Id", "UserName", assignment.TrainerID);
            ViewData["ClassID"] = new SelectList(_context.Classes, "ClassID", "ClassName", assignment.ClassID);
            ViewData["ModuleID"] = new SelectList(_context.Modules, "ModuleID", "ModuleName", assignment.ModuleID);
            return View(assignment);
        }

        // GET: Admin/Assignments/Edit/5
        public async Task<IActionResult> Edit(int? classid, int? moduleid, string trainerid) {
            if(classid == null || moduleid == null || trainerid == null) {
                return NotFound();
            }

            var assignment = await _context.Assignment.FindAsync(classid, moduleid, trainerid);
            if(assignment == null) {
                return NotFound();
            }
            ViewData["TrainerID"] = new SelectList(_context.Users, "Id", "UserName", assignment.TrainerID);
            ViewData["ClassID"] = new SelectList(_context.Classes, "ClassID", "ClassName", assignment.ClassID);
            ViewData["ModuleID"] = new SelectList(_context.Modules, "ModuleID", "ModuleName", assignment.ModuleID);
            return View(assignment);
        }

        // POST: Admin/Assignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int classid, int moduleid, string trainerid, [Bind("ClassID,ModuleID,TrainerID,RegistrationCode")] Assignment assignment) {
            if(classid != assignment.ClassID) {
                return NotFound();
            }
            if(moduleid != assignment.ModuleID)
                return NotFound();
            if(trainerid != assignment.TrainerID)
                return NotFound();

            if(ModelState.IsValid) {
                try {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                } catch(DbUpdateConcurrencyException) {
                    if(!AssignmentExists(assignment.ClassID, assignment.ModuleID, assignment.TrainerID)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainerID"] = new SelectList(_context.Users, "Id", "UserName", assignment.TrainerID);
            ViewData["ClassID"] = new SelectList(_context.Classes, "ClassID", "ClassName", assignment.ClassID);
            ViewData["ModuleID"] = new SelectList(_context.Modules, "ModuleID", "ModuleName", assignment.ModuleID);
            return View(assignment);
        }

        // GET: Admin/Assignments/Delete/5
        public async Task<IActionResult> Delete(int? classid, int? moduleid, string trainerid) {
            if(classid == null || moduleid == null || trainerid == null) {
                return NotFound();
            }

            var assignment = await _context.Assignment
                .Include(a => a.Admin)
                .Include(a => a.Class)
                .Include(a => a.Module)
                .FirstOrDefaultAsync(m => m.ClassID == classid);
            if(assignment == null) {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: Admin/Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int classid, int moduleid, string trainerid) {
            var assignment = await _context.Assignment.FindAsync(classid, moduleid, trainerid);
            _context.Assignment.Remove(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int classid, int moduleid, string trainerid) {
            return _context.Assignment.Any(e => e.ClassID == classid);
        }
    }
}