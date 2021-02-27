//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using FS.Areas.Admin.Models;
//using FS.Data;
//using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Identity;
//using FS.Models;

//namespace FS.Areas.Admin.Controllers {
//    [Area("Admin")]
//    public class FeedbacksController : Controller {
//        private readonly AppDbContext _context;
//        private readonly ILogger<FeedbacksController> _logger;
//        private readonly UserManager<AppUser> _usermanager;

//        public FeedbacksController(AppDbContext context,
//            ILogger<FeedbacksController> logger,
//             UserManager<AppUser> usermanager) {
//            _context = context;
//            _logger = logger;
//            _usermanager = usermanager;
//        }

//        // GET: Admin/Feedbacks
//        public async Task<IActionResult> Index() {
//            var appDbContext = _context.Feedbacks.Include(f => f.Id).Include(f => f.TypeFeedbacks);
//            return View(await appDbContext.ToListAsync());
//        }

//        // GET: Admin/Feedbacks/Details/5
//        public async Task<IActionResult> Details(int? id) {
//            if(id == null) {
//                return NotFound();
//            }

//            var feedback = await _context.Feedbacks
//                .Include(f => f.Id)
//                .Include(f => f.TypeFeedbacks)
//                .FirstOrDefaultAsync(m => m.FeedbackId == id);
//            if(feedback == null) {
//                return NotFound();
//            }

//            return View(feedback);
//        }

//        // GET: Admin/Feedbacks/Create
//        [BindProperty]
//        public int[] selectedQuestions { set; get; }

//        public async Task<IActionResult> Create() {
//            // Thông tin về User tạo Post
//            var user = await _usermanager.GetUserAsync(User);
//            ViewData["userpost"] = $"{user.UserName} {user.FullName}";
//            ViewData["AdminID"] = new SelectList(_context.Users, "Id", "UserName");
//            ViewData["TypeFeedbackId"] = new SelectList(_context.TypeFeedback, "TypeId", "TypeName");

//            return View();
//        }

//        // POST: Admin/Feedbacks/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("FeedbackId,Title,AdminID,IsDeleted,TypeFeedbackId")] Feedback feedback) {
//            // Thông tin về User tạo Post
//            var user = await _usermanager.GetUserAsync(User);
//            ViewData["userpost"] = $"{user.UserName} {user.FullName}";
//            if(ModelState.IsValid) {
//                var newfeedback = new Feedback() {
//                    Title = feedback.Title,
//                    AdminID = user.Id,
//                    TypeFeedbackId = feedback.TypeFeedbackId
//                };
//                _context.Add(newfeedback);
//                await _context.SaveChangesAsync();

//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["AdminID"] = new SelectList(_context.Users, "Id", "UserName", feedback.AdminID);
//            ViewData["TypeFeedbackId"] = new SelectList(_context.TypeFeedback, "TypeId", "TypeName", feedback.TypeFeedbackId);
//            return View(feedback);
//        }

//        // GET: Admin/Feedbacks/Edit/5
//        public async Task<IActionResult> Edit(int? id) {
//            if(id == null) {
//                return NotFound();
//            }

//            var feedback = await _context.Feedbacks.FindAsync(id);
//            if(feedback == null) {
//                return NotFound();
//            }
//            ViewData["AdminID"] = new SelectList(_context.Users, "Id", "UserName", feedback.AdminID);
//            ViewData["TypeFeedbackId"] = new SelectList(_context.TypeFeedback, "TypeId", "TypeName", feedback.TypeFeedbackId);
//            return View(feedback);
//        }

//        // POST: Admin/Feedbacks/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("FeedbackId,Title,AdminID,IsDeleted,TypeFeedbackId")] Feedback feedback) {
//            if(id != feedback.FeedbackId) {
//                return NotFound();
//            }

//            if(ModelState.IsValid) {
//                try {
//                    _context.Update(feedback);
//                    await _context.SaveChangesAsync();
//                } catch(DbUpdateConcurrencyException) {
//                    if(!FeedbackExists(feedback.FeedbackId)) {
//                        return NotFound();
//                    } else {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["AdminID"] = new SelectList(_context.Users, "Id", "UserName", feedback.AdminID);
//            ViewData["TypeFeedbackId"] = new SelectList(_context.TypeFeedback, "TypeId", "TypeName", feedback.TypeFeedbackId);
//            return View(feedback);
//        }

//        // GET: Admin/Feedbacks/Delete/5
//        public async Task<IActionResult> Delete(int? id) {
//            if(id == null) {
//                return NotFound();
//            }

//            var feedback = await _context.Feedbacks
//                .Include(f => f.Id)
//                .Include(f => f.TypeFeedbacks)
//                .FirstOrDefaultAsync(m => m.FeedbackId == id);
//            if(feedback == null) {
//                return NotFound();
//            }

//            return View(feedback);
//        }

//        // POST: Admin/Feedbacks/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id) {
//            var feedback = await _context.Feedbacks.FindAsync(id);
//            _context.Feedbacks.Remove(feedback);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool FeedbackExists(int id) {
//            return _context.Feedbacks.Any(e => e.FeedbackId == id);
//        }
//    }
//}