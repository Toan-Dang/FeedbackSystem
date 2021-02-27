using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FS.Areas.Admin.Models;
using FS.Data;
using Microsoft.AspNetCore.Identity;
using FS.Models;
using Microsoft.Extensions.Logging;

namespace FS.Areas.Admin.Controllers {

    [Area("Admin")]
    public class FeedbacksController : Controller {
        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _usermanager;

        private readonly ILogger<FeedbacksController> _logger;

        public FeedbacksController(AppDbContext context,
            UserManager<AppUser> usermanager,
            ILogger<FeedbacksController> logger) {
            _context = context;
            _usermanager = usermanager;
            _logger = logger;
        }

        // GET: Admin/Feedbacks
        public async Task<IActionResult> Index() {
            var appDbContext = _context.Feedbacks.Include(f => f.Id).Include(f => f.TypeFeedbacks)
                .Include(f => f.Feedback_Questions)
                .ThenInclude(f => f.Question);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Feedbacks/Details/5
        public async Task<IActionResult> Details(int? id) {
            if(id == null) {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Id)
                .Include(f => f.TypeFeedbacks)
                .Include(f => f.Feedback_Questions)
                .ThenInclude(f => f.Question)
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if(feedback == null) {
                return NotFound();
            }
            var questions = await _context.Questions.ToListAsync();
            ViewData["questions"] = new MultiSelectList(questions, "QuestionID", "QuestionContent", selectedQuestion);
            return View(feedback);
        }

        // GET: Admin/Feedbacks/Create
        [BindProperty]
        public int[] selectedQuestion { set; get; }

        public async Task<IActionResult> Create() {
            // Thông tin về User tạo Post
            var user = await _usermanager.GetUserAsync(User);
            ViewData["userpost"] = $"{user.UserName} {user.FullName}";
            // Danh mục chọn để tick question, tạo MultiSelectList
            var questions = await _context.Questions.ToListAsync();
            ViewData["questions"] = new MultiSelectList(questions, "QuestionID", "QuestionContent");
            //  var topic = await _context.Topics.ToListAsync();
            // ViewData["topic"] = new SelectList(topic, "TopicID", "TopicName");

            ViewData["AdminID"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["TypeFeedbackId"] = new SelectList(_context.TypeFeedback, "TypeId", "TypeName");
            return View();
        }

        // POST: Admin/Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeedbackId,Title,AdminID,IsDeleted,TypeFeedbackId")] Feedback feedback) {
            var user = await _usermanager.GetUserAsync(User);
            ViewData["userpost"] = $"{user.UserName} {user.FullName}";

            if(ModelState.IsValid) {
                var newfeedback = new Feedback() {
                    Title = feedback.Title,
                    AdminID = user.Id,
                    TypeFeedbackId = feedback.TypeFeedbackId
                };

                _context.Add(newfeedback);
                await _context.SaveChangesAsync();
                // Chèn thông tin về Feedback-Question của bài Post
                foreach(var selectedQuestionz in selectedQuestion) {
                    _context.Add(new Feedback_Question() { FeedbackID = newfeedback.FeedbackId, QuestionID = selectedQuestionz });
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminID"] = new SelectList(_context.Users, "Id", "UserName", feedback.AdminID);
            ViewData["TypeFeedbackId"] = new SelectList(_context.TypeFeedback, "TypeId", "TypeName", feedback.TypeFeedbackId);
            return View(feedback);
        }

        // GET: Admin/Feedbacks/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if(id == null) {
                return NotFound();
            }

            //  var feedback = await _context.Feedbacks.FindAsync(id);
            var feedback = await _context.Feedbacks.Where(p => p.FeedbackId == id)
                .Include(p => p.Id)
                .Include(p => p.TypeFeedbacks)
                .Include(p => p.Feedback_Questions)
                .ThenInclude(p => p.Question).FirstOrDefaultAsync();

            if(feedback == null) {
                return NotFound();
            }
            // Thông tin về User tạo Post
            var user = await _usermanager.GetUserAsync(User);
            ViewData["userpost"] = $"{user.UserName} {user.FullName}";
            // Danh mục chọn để tick question, tạo MultiSelectList

            var selectedCates = feedback.Feedback_Questions.Select(c => c.Question).ToArray();
            var questions = await _context.Questions.ToListAsync();
            ViewData["questions"] = new MultiSelectList(questions, "QuestionID", "QuestionContent", selectedCates);

            ViewData["AdminID"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["TypeFeedbackId"] = new SelectList(_context.TypeFeedback, "TypeId", "TypeName");

            return View(feedback);

            // Danh mục chọn
        }

        // POST: Admin/Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeedbackId,Title,AdminID,IsDeleted,TypeFeedbackId")] Feedback feedback) {
            if(id != feedback.FeedbackId) {
                return NotFound();
            }
            // Thông tin về User sửa Post
            var user = await _usermanager.GetUserAsync(User);
            ViewData["userpost"] = $"{user.UserName} {user.FullName}";

            if(ModelState.IsValid) {
                // Lấy nội dung từ DB
                var postUpdate = await _context.Feedbacks.Where(p => p.FeedbackId == id)
                .Include(p => p.Id)
                .Include(p => p.TypeFeedbacks)
                .Include(p => p.Feedback_Questions)
                .ThenInclude(p => p.Question).FirstOrDefaultAsync();
                if(postUpdate == null) {
                    return NotFound();
                }

                // Cập nhật nội dung mới
                postUpdate.Title = feedback.Title;
                postUpdate.TypeFeedbackId = feedback.TypeFeedbackId;
                postUpdate.AdminID = user.Id;

                // Các danh mục không có trong selectedCategories
                var listcateremove = postUpdate.Feedback_Questions
                                               .Where(p => !selectedQuestion.Contains(p.QuestionID))
                                               .ToList();
                listcateremove.ForEach(c => postUpdate.Feedback_Questions.Remove(c));

                // Các ID category chưa có trong postUpdate.PostCategories
                var listCateAdd = selectedQuestion
                                    .Where(
                                        id => !postUpdate.Feedback_Questions.Where(c => c.QuestionID == id).Any()
                                    ).ToList();

                listCateAdd.ForEach(id => {
                    postUpdate.Feedback_Questions.Add(new Feedback_Question() {
                        FeedbackID = postUpdate.FeedbackId,
                        QuestionID = id
                    });
                });
                try {
                    _context.Update(postUpdate);
                    await _context.SaveChangesAsync();
                } catch(DbUpdateConcurrencyException) {
                    if(!FeedbackExists(feedback.FeedbackId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var questions = await _context.Questions.ToListAsync();
            ViewData["questions"] = new MultiSelectList(questions, "QuestionID", "QuestionContent", selectedQuestion);

            ViewData["AdminID"] = new SelectList(_context.Users, "Id", "UserName", feedback.AdminID);
            ViewData["TypeFeedbackId"] = new SelectList(_context.TypeFeedback, "TypeId", "TypeName", feedback.TypeFeedbackId);
            return View(feedback);
        }

        // GET: Admin/Feedbacks/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if(id == null) {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Id)
                .Include(f => f.TypeFeedbacks)
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if(feedback == null) {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Admin/Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var feedback = await _context.Feedbacks.FindAsync(id);
            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackExists(int id) {
            return _context.Feedbacks.Any(e => e.FeedbackId == id);
        }
    }
}