//using FS.Areas.Admin.Models;
using FS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FS.Areas.Admin.Models;

namespace FS.Data {

    public class AppDbContext : IdentityDbContext<AppUser> {
        public DbSet<Module> Modules { set; get; }

        public DbSet<TypeFeedback> TypeFeedback { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }
        public DbSet<Assignment> Assignments { set; get; }
        public DbSet<Class> Classes { set; get; }
        public DbSet<Question> Questions { set; get; }
        public DbSet<Topic> Topics { set; get; }
        public DbSet<Feedback_Question> Feedback_Questions { set; get; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            // Bỏ tiền tố AspNet của các bảng: mặc định các bảng trong IdentityDbContext có
            // tên với tiền tố AspNet như: AspNetUserRoles, AspNetUser ...
            // Đoạn mã sau chạy khi khởi tạo DbContext, tạo database sẽ loại bỏ tiền tố đó
            foreach(var entityType in builder.Model.GetEntityTypes()) {
                var tableName = entityType.GetTableName();
                if(tableName.StartsWith("AspNet")) {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            builder.Entity<Assignment>().HasKey(p => new { p.ClassID, p.ModuleID, p.TrainerID });
            builder.Entity<Feedback_Question>().HasKey(p => new { p.FeedbackID, p.QuestionID });
        }

        //public DbSet<Module> Module { set; get; }

        public DbSet<FS.Areas.Admin.Models.Module> Module { get; set; }

        //public DbSet<Module> Module { set; get; }
        public DbSet<FS.Areas.Admin.Models.TypeFeedback> TypeFeedbacks { get; set; }

        public DbSet<FS.Areas.Admin.Models.Class> Class { get; set; }
        public DbSet<FS.Areas.Admin.Models.Feedback> Feedback { get; set; }
        public DbSet<FS.Areas.Admin.Models.Assignment> Assignment { get; set; }
        public DbSet<FS.Areas.Admin.Models.Feedback_Question> Feedback_Question { get; set; }
        public DbSet<FS.Areas.Admin.Models.Topic> Topic { get; set; }
        public DbSet<FS.Areas.Admin.Models.Question> Question { get; set; }
    }
}