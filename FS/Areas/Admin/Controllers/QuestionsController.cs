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
    public class QuestionsController : Controller {
        private readonly AppDbContext _context;
        private readonly ILogger<QuestionsController> _logger;
        private readonly UserManager<AppUser> _usermanager;

        public QuestionsController(AppDbContext context,
            ILogger<QuestionsController> logger,
             UserManager<AppUser> usermanager) {
            _context = context;
            _logger = logger;
            _usermanager = usermanager;
        }

        public const int ITEMS_PER_PAGE = 10;

        // GET: Admin/Questions
        public async Task<IActionResult> Index([Bind(Prefix = "page")] int pageNumber) {
            if(pageNumber == 0)
                pageNumber = 1;
            var appDbContext = _context.Questions.Include(q => q.Topic);
            _logger.LogInformation(pageNumber.ToString());
            // Lấy tổng số dòng dữ liệu
            var totalItems = appDbContext.Count();
            // Tính số trang hiện thị (mỗi trang hiện thị ITEMS_PER_PAGE mục)
            int totalPages = (int)Math.Ceiling((double)totalItems / ITEMS_PER_PAGE);

            if(pageNumber > totalPages)
                return RedirectToAction(nameof(QuestionsController.Index), new { page = totalPages });

            var posts = await appDbContext
                .Skip(ITEMS_PER_PAGE * (pageNumber - 1))
                .Take(ITEMS_PER_PAGE)
                .ToListAsync();

            ViewData["pageNumber"] = pageNumber;
            ViewData["totalPages"] = totalPages;
            ViewData["TopicID"] = new SelectList(_context.Topics, "TopicID", "TopicName");
            return View(posts.AsEnumerable());
        }

        // GET: Admin/Questions/Details/5
        public async Task<IActionResult> Details(int? id) {
            if(id == null) {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Topic)
                .FirstOrDefaultAsync(m => m.QuestionID == id);
            if(question == null) {
                return NotFound();
            }

            return View(question);
        }

        // GET: Admin/Questions/Create
        public IActionResult Create() {
            ViewData["TopicID"] = new SelectList(_context.Topics, "TopicID", "TopicName");
            return View();
        }

        // POST: Admin/Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionID,TopicID,QuestionContent,IsDeleted")] Question question) {
            if(ModelState.IsValid) {
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicID"] = new SelectList(_context.Topics, "TopicID", "TopicName", question.TopicID);
            return View(question);
        }

        // GET: Admin/Questions/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if(id == null) {
                return NotFound();
            }

            var question = await _context.Questions.FindAsync(id);
            if(question == null) {
                return NotFound();
            }
            ViewData["TopicID"] = new SelectList(_context.Topics, "TopicID", "TopicName", question.TopicID);
            return View(question);
        }

        // POST: Admin/Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionID,TopicID,QuestionContent,IsDeleted")] Question question) {
            if(id != question.QuestionID) {
                return NotFound();
            }

            if(ModelState.IsValid) {
                try {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                } catch(DbUpdateConcurrencyException) {
                    if(!QuestionExists(question.QuestionID)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicID"] = new SelectList(_context.Topics, "TopicID", "TopicName", question.TopicID);
            return View(question);
        }

        // GET: Admin/Questions/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if(id == null) {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Topic)
                .FirstOrDefaultAsync(m => m.QuestionID == id);
            if(question == null) {
                return NotFound();
            }

            return View(question);
        }

        // POST: Admin/Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id) {
            return _context.Questions.Any(e => e.QuestionID == id);
        }
    }
}