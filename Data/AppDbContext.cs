using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApi.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppAppraisals> AppAppraisals { get; set; }
        public virtual DbSet<AppGoals> AppGoals { get; set; }
        public virtual DbSet<AppRoles> AppRoles { get; set; }
        public virtual DbSet<AppUserRoles> AppUserRoles { get; set; }
        public virtual DbSet<AppUsers> AppUsers { get; set; }
        public virtual DbSet<AppraisalGoals> AppraisalGoals { get; set; }
        public virtual DbSet<AppraisalGoalsObjectives> AppraisalGoalsObjectives { get; set; }
        public virtual DbSet<AppraisalObjectives> AppraisalObjectives { get; set; }
        public virtual DbSet<Appraisals> Appraisals { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=;database=hris");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppAppraisals>(entity =>
            {
                entity.HasKey(e => e.AppraisalId)
                    .HasName("PRIMARY");

                entity.ToTable("app_appraisals");

                entity.HasIndex(e => e.AppraisedBy)
                    .HasName("appraised_by");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("created_by");

                entity.HasIndex(e => e.CreatorDeptId)
                    .HasName("creator_dept_id");

                entity.Property(e => e.AppraisalId)
                    .HasColumnName("appraisal_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AppraisalRating)
                    .HasColumnName("appraisal_rating")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AppraisalRemarks)
                    .HasColumnName("appraisal_remarks")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AppraisedBy)
                    .HasColumnName("appraised_by")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreatorDeptId)
                    .HasColumnName("creator_dept_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreatorJobTitle)
                    .HasColumnName("creator_job_title")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateAppraised)
                    .HasColumnName("date_appraised")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ReviewYear)
                    .HasColumnName("review_year")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.AppAppraisals)
                    .HasPrincipalKey(p => p.Username)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("app_appraisals_ibfk_1");

                entity.HasOne(d => d.CreatorDept)
                    .WithMany(p => p.AppAppraisals)
                    .HasForeignKey(d => d.CreatorDeptId)
                    .HasConstraintName("app_appraisals_ibfk_3");
            });

            modelBuilder.Entity<AppGoals>(entity =>
            {
                entity.HasKey(e => e.GoalId)
                    .HasName("PRIMARY");

                entity.ToTable("app_goals");

                entity.HasIndex(e => e.AppraisalId)
                    .HasName("appraisal_id");

                entity.Property(e => e.GoalId)
                    .HasColumnName("goal_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Activity)
                    .HasColumnName("activity")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AppraisalId)
                    .HasColumnName("appraisal_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Goal)
                    .IsRequired()
                    .HasColumnName("goal")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Measures)
                    .HasColumnName("measures")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Appraisal)
                    .WithMany(p => p.AppGoals)
                    .HasForeignKey(d => d.AppraisalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("app_goals_ibfk_1");
            });

            modelBuilder.Entity<AppRoles>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PRIMARY");

                entity.ToTable("app_roles");

                entity.Property(e => e.RoleId)
                    .HasColumnName("roleId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Rolename)
                    .IsRequired()
                    .HasColumnName("rolename")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AppUserRoles>(entity =>
            {
                entity.ToTable("app_user_roles");

                entity.HasIndex(e => e.Roleid)
                    .HasName("roleid");

                entity.HasIndex(e => e.Userid)
                    .HasName("userid");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Roleid)
                    .HasColumnName("roleid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AppUserRoles)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("app_user_roles_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AppUserRoles)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("app_user_roles_ibfk_1");
            });

            modelBuilder.Entity<AppUsers>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("app_users");

                entity.HasIndex(e => e.Username)
                    .HasName("username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Enabled)
                    .HasColumnName("enabled")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Host)
                    .HasColumnName("host")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LastLoggedIn)
                    .HasColumnName("lastLoggedIn")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AppraisalGoals>(entity =>
            {
                entity.HasKey(e => e.GoalId)
                    .HasName("PRIMARY");

                entity.ToTable("appraisal_goals");

                entity.Property(e => e.GoalId)
                    .HasColumnName("goal_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''''''");

                entity.Property(e => e.Goal)
                    .HasColumnName("goal")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''''''");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<AppraisalGoalsObjectives>(entity =>
            {
                entity.ToTable("appraisal_goals_objectives");

                entity.HasIndex(e => e.AppraisalId)
                    .HasName("appraisal_id");

                entity.HasIndex(e => e.GoalId)
                    .HasName("goal_id");

                entity.HasIndex(e => e.ObjectiveId)
                    .HasName("objective_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AppraisalId)
                    .HasColumnName("appraisal_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GoalId)
                    .HasColumnName("goal_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ObjectiveId)
                    .HasColumnName("objective_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Appraisal)
                    .WithMany(p => p.AppraisalGoalsObjectives)
                    .HasForeignKey(d => d.AppraisalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appraisal_goals_objectives_ibfk_1");

                entity.HasOne(d => d.Goal)
                    .WithMany(p => p.AppraisalGoalsObjectives)
                    .HasForeignKey(d => d.GoalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appraisal_goals_objectives_ibfk_2");

                entity.HasOne(d => d.Objective)
                    .WithMany(p => p.AppraisalGoalsObjectives)
                    .HasForeignKey(d => d.ObjectiveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appraisal_goals_objectives_ibfk_3");
            });

            modelBuilder.Entity<AppraisalObjectives>(entity =>
            {
                entity.HasKey(e => e.ObjectiveId)
                    .HasName("PRIMARY");

                entity.ToTable("appraisal_objectives");

                entity.Property(e => e.ObjectiveId)
                    .HasColumnName("objective_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''''''");

                entity.Property(e => e.Objective)
                    .HasColumnName("objective")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("''''''");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Appraisals>(entity =>
            {
                entity.HasKey(e => e.AppraisalId)
                    .HasName("PRIMARY");

                entity.ToTable("appraisals");

                entity.HasIndex(e => e.AppraisedBy)
                    .HasName("appraised_by");

                entity.HasIndex(e => e.CreatedBy)
                    .HasName("created_by");

                entity.Property(e => e.AppraisalId)
                    .HasColumnName("appraisal_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AppraisalRating)
                    .HasColumnName("appraisal_rating")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AppraisalRemarks)
                    .HasColumnName("appraisal_remarks")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AppraisedBy)
                    .HasColumnName("appraised_by")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateAppraised)
                    .HasColumnName("date_appraised")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ReviewYear)
                    .HasColumnName("review_year")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.AppraisedByNavigation)
                    .WithMany(p => p.AppraisalsAppraisedByNavigation)
                    .HasPrincipalKey(p => p.Username)
                    .HasForeignKey(d => d.AppraisedBy)
                    .HasConstraintName("appraisals_ibfk_2");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.AppraisalsCreatedByNavigation)
                    .HasPrincipalKey(p => p.Username)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("appraisals_ibfk_1");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptId)
                    .HasName("PRIMARY");

                entity.ToTable("department");

                entity.Property(e => e.DeptId)
                    .HasColumnName("DeptID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff");

                entity.HasIndex(e => e.DeptId)
                    .HasName("staff_dept_fk");

                entity.HasIndex(e => e.Username)
                    .HasName("username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.StaffId)
                    .HasColumnName("StaffID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.DateJoined)
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DeptId)
                    .HasColumnName("DeptID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Enabled)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OtherNames)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Position)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.PositionLevel)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Supervisor)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.DeptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("staff_dept_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
