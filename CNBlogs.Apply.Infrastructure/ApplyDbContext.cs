using CNBlogs.Apply.Domain;
using CNBlogs.Apply.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNBlogs.Apply.Infrastructure
{
    public class ApplyDbContext : DbContext, IDbContext
    {
        public ApplyDbContext()
            : base("name=cnblogs_apply")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JsPermissionApply>()
                .HasKey(x => x.Id)
                .ToTable("JsPermissionApplys");
            modelBuilder.Entity<JsPermissionApply>()
                .Property(x => x.Reason).HasMaxLength(3000).IsRequired();
            modelBuilder.Entity<JsPermissionApply>()
                .Property(x => x.Ip).HasMaxLength(50);
            modelBuilder.Entity<JsPermissionApply>()
                .Property(x => x.ReplyContent).HasMaxLength(3000);
            modelBuilder.Entity<JsPermissionApply>()
               .Property(x => x.ApprovedTime).IsOptional();
            modelBuilder.Entity<JsPermissionApply>()
                .Property(x => x.User.LoginName).HasMaxLength(128);
            modelBuilder.Entity<JsPermissionApply>()
                .Property(x => x.User.DisplayName).HasMaxLength(128);
            modelBuilder.Entity<JsPermissionApply>()
               .Property(x => x.User.Alias).HasMaxLength(50);
            modelBuilder.Entity<JsPermissionApply>()
               .Property(x => x.User.Email).HasMaxLength(128);

            base.OnModelCreating(modelBuilder);
        }
    }
}
