using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Azzuha.Data.DTOs;

namespace Azzuha.Data.Context
{
    public partial class AzzuhaDbContext : DbContext
    {
        public AzzuhaDbContext()
        {
        }

        public AzzuhaDbContext(DbContextOptions<AzzuhaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AboutUs> AboutUs { get; set; }
        public virtual DbSet<AdminSystemConfigurations> AdminSystemConfigurations { get; set; }
        public virtual DbSet<AggregatedCounter> AggregatedCounter { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AssignedRights> AssignedRights { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<ContactQueries> ContactQueries { get; set; }
        public virtual DbSet<ContentType> ContentType { get; set; }
        public virtual DbSet<Contents> Contents { get; set; }
        public virtual DbSet<Counter> Counter { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<Courses1> Courses1 { get; set; }
        public virtual DbSet<CoursesAdmission> CoursesAdmission { get; set; }
        public virtual DbSet<CoursesAdmission1> CoursesAdmission1 { get; set; }
        public virtual DbSet<DeviceTypes> DeviceTypes { get; set; }
        public virtual DbSet<EventTypes> EventTypes { get; set; }
        public virtual DbSet<Gallery> Gallery { get; set; }
        public virtual DbSet<GalleryImages> GalleryImages { get; set; }
        public virtual DbSet<Genders> Genders { get; set; }
        public virtual DbSet<Hash> Hash { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobParameter> JobParameter { get; set; }
        public virtual DbSet<JobQueue> JobQueue { get; set; }
        public virtual DbSet<JsonData> JsonData { get; set; }
        public virtual DbSet<List> List { get; set; }
        public virtual DbSet<MainSlider> MainSlider { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<MediaTypes> MediaTypes { get; set; }
        public virtual DbSet<PhoneCountryCode> PhoneCountryCode { get; set; }
        public virtual DbSet<PrivacyTypes> PrivacyTypes { get; set; }
        public virtual DbSet<RoleSrights> RoleSrights { get; set; }
        public virtual DbSet<Schema> Schema { get; set; }
        public virtual DbSet<Server> Server { get; set; }
        public virtual DbSet<Set> Set { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<SubscriptionUserInfos> SubscriptionUserInfos { get; set; }
        public virtual DbSet<UpComingEvents> UpComingEvents { get; set; }
        public virtual DbSet<UserAddresses> UserAddresses { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Initial Catalog=azzuhais_sd;user id=azzuhais_sa;password=_Muse@123;Data Source=66.165.248.146;MultipleActiveResultSets=True", x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "azzuhais_sa");

            modelBuilder.Entity<AboutUs>(entity =>
            {
                entity.ToTable("AboutUs", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AdminSystemConfigurations>(entity =>
            {
                entity.ToTable("AdminSystemConfigurations", "dbo");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModfityBy).HasMaxLength(50);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK_HangFire_CounterAggregated");

                entity.ToTable("AggregatedCounter", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_AggregatedCounter_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.ToTable("AspNetRoleClaims", "dbo");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.ToTable("AspNetRoles", "dbo");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.ToTable("AspNetUserClaims", "dbo");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.ToTable("AspNetUserLogins", "dbo");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("AspNetUserRoles", "dbo");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.ToTable("AspNetUserTokens", "dbo");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.ToTable("AspNetUsers", "dbo");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.EnableFlag)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.EnalbleFalg)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.IsBlock)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UpdationDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AssignedRights>(entity =>
            {
                entity.ToTable("AssignedRights", "dbo");

                entity.Property(e => e.RightsId).HasColumnName("Rights_id");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasColumnName("Role_Id")
                    .HasMaxLength(128);

                entity.HasOne(d => d.Rights)
                    .WithMany(p => p.AssignedRights)
                    .HasForeignKey(d => d.RightsId)
                    .HasConstraintName("FK_AssignedRights_RoleSRights");
            });

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.ToTable("Cities", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_at")
                    .HasDefaultValueSql("('2014-01-01 01:01:01')");

                entity.Property(e => e.Flag).HasDefaultValueSql("((0))");

                entity.Property(e => e.Latitude).HasColumnType("decimal(10, 8)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(11, 8)");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StateCode)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("Updated_on")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.WikiDataId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Cities_Countries");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_Cities_States");
            });

            modelBuilder.Entity<ContactQueries>(entity =>
            {
                entity.ToTable("ContactQueries", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.PhoneNumber).HasMaxLength(100);

                entity.Property(e => e.Query)
                    .IsRequired()
                    .HasMaxLength(1500);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ContentType>(entity =>
            {
                entity.ToTable("ContentType", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Contents>(entity =>
            {
                entity.ToTable("Contents", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.Contents)
                    .HasForeignKey(d => d.ContentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contents_Contents");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Counter", "HangFire");

                entity.HasIndex(e => e.Key)
                    .HasName("CX_HangFire_Counter")
                    .IsClustered();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.ToTable("Countries", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Capital)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasColumnName("Created_at");

                entity.Property(e => e.Currency)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Emoji)
                    .HasMaxLength(191)
                    .IsUnicode(false);

                entity.Property(e => e.EmojiU)
                    .HasMaxLength(191)
                    .IsUnicode(false);

                entity.Property(e => e.Iso2)
                    .HasColumnName("ISO2")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Iso3)
                    .HasColumnName("ISO3")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Native)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneCode)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_at")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.WikiDataId)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Courses>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CourseName).HasMaxLength(50);

                entity.Property(e => e.CourseType).HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Courses1>(entity =>
            {
                entity.ToTable("Courses", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CourseName).HasMaxLength(50);

                entity.Property(e => e.CourseType).HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<CoursesAdmission>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BformUrl).HasColumnName("BFormURL");

                entity.Property(e => e.Cnicurl).HasColumnName("CNICURL");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Dob).HasColumnName("DOB");

                entity.Property(e => e.EmailAddress).HasMaxLength(150);

                entity.Property(e => e.FatherName).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.PeducationFrom).HasColumnName("PEducationFrom");

                entity.Property(e => e.PhoneNumber).HasMaxLength(150);

                entity.Property(e => e.PrevoiusEducation).IsRequired();

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CoursesAdmission)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_CoursesAdmission_Courses");
            });

            modelBuilder.Entity<CoursesAdmission1>(entity =>
            {
                entity.ToTable("CoursesAdmission", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BformUrl).HasColumnName("BFormURL");

                entity.Property(e => e.Cnicurl).HasColumnName("CNICURL");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.EmailAddress).HasMaxLength(150);

                entity.Property(e => e.FatherName).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.PeducationFrom).HasColumnName("PEducationFrom");

                entity.Property(e => e.PhoneNumber).HasMaxLength(150);

                entity.Property(e => e.PrevoiusEducation).IsRequired();

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<DeviceTypes>(entity =>
            {
                entity.ToTable("DeviceTypes", "dbo");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<EventTypes>(entity =>
            {
                entity.ToTable("EventTypes", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.EventName).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.ToTable("Gallery", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<GalleryImages>(entity =>
            {
                entity.ToTable("GalleryImages", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Gallery)
                    .WithMany(p => p.GalleryImages)
                    .HasForeignKey(d => d.GalleryId)
                    .HasConstraintName("FK_GalleryImages_Gallery");
            });

            modelBuilder.Entity<Genders>(entity =>
            {
                entity.ToTable("Genders", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Field })
                    .HasName("PK_HangFire_Hash");

                entity.ToTable("Hash", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_Hash_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Field).HasMaxLength(100);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.HasIndex(e => e.StateName)
                    .HasName("IX_HangFire_Job_StateName")
                    .HasFilter("([StateName] IS NOT NULL)");

                entity.HasIndex(e => new { e.StateName, e.ExpireAt })
                    .HasName("IX_HangFire_Job_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Arguments).IsRequired();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.InvocationData).IsRequired();

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Name })
                    .HasName("PK_HangFire_JobParameter");

                entity.ToTable("JobParameter", "HangFire");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameter)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_JobParameter_Job");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.HasKey(e => new { e.Queue, e.Id })
                    .HasName("PK_HangFire_JobQueue");

                entity.ToTable("JobQueue", "HangFire");

                entity.Property(e => e.Queue).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<JsonData>(entity =>
            {
                entity.ToTable("JsonData", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Id })
                    .HasName("PK_HangFire_List");

                entity.ToTable("List", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_List_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<MainSlider>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Heading).HasMaxLength(1000);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.ToTable("Media", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.MediaType)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.MediaTypeId)
                    .HasConstraintName("FK_Media_MediaTypes");
            });

            modelBuilder.Entity<MediaTypes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<PhoneCountryCode>(entity =>
            {
                entity.ToTable("PhoneCountryCode", "dbo");

                entity.Property(e => e.CountryCode).HasMaxLength(50);

                entity.Property(e => e.CountryName).HasMaxLength(500);

                entity.Property(e => e.DialCode)
                    .HasColumnName("dial_code")
                    .HasMaxLength(50);

                entity.Property(e => e.HighResulationImageUrl)
                    .HasColumnName("HighResulationImageURL")
                    .HasMaxLength(500);

                entity.Property(e => e.LowResulationImageUrl1)
                    .HasColumnName("LowResulationImageURL1")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<PrivacyTypes>(entity =>
            {
                entity.ToTable("PrivacyTypes", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Privacy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<RoleSrights>(entity =>
            {
                entity.ToTable("RoleSRights", "dbo");

                entity.Property(e => e.CreatedBy).HasMaxLength(128);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FkSelf).HasColumnName("FK_Self");

                entity.Property(e => e.Logo).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.UpdatedBy).HasMaxLength(128);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_HangFire_Schema");

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.HasIndex(e => e.LastHeartbeat)
                    .HasName("IX_HangFire_Server_LastHeartbeat");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Value })
                    .HasName("PK_HangFire_Set");

                entity.ToTable("Set", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_Set_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.HasIndex(e => new { e.Key, e.Score })
                    .HasName("IX_HangFire_Set_Score");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Id })
                    .HasName("PK_HangFire_State");

                entity.ToTable("State", "HangFire");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.State)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });

            modelBuilder.Entity<States>(entity =>
            {
                entity.ToTable("States", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedAt).HasColumnName("Created_at");

                entity.Property(e => e.FipsCode)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Flag).HasDefaultValueSql("((0))");

                entity.Property(e => e.Iso2)
                    .HasColumnName("ISO2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");

                entity.Property(e => e.WikiDataId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_States_Countries");
            });

            modelBuilder.Entity<SubscriptionUserInfos>(entity =>
            {
                entity.ToTable("SubscriptionUserInfos", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("date");

                entity.Property(e => e.StripCode).IsRequired();

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SubscriptionUserInfos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubscriptionUserInfos_Users");
            });

            modelBuilder.Entity<UpComingEvents>(entity =>
            {
                entity.ToTable("UpComingEvents", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.EventType)
                    .WithMany(p => p.UpComingEvents)
                    .HasForeignKey(d => d.EventTypeId)
                    .HasConstraintName("FK_UpComingEvents_EventTypes");
            });

            modelBuilder.Entity<UserAddresses>(entity =>
            {
                entity.ToTable("UserAddresses", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address1).HasMaxLength(300);

                entity.Property(e => e.Address2).HasMaxLength(500);

                entity.Property(e => e.AddressLatitude).HasMaxLength(50);

                entity.Property(e => e.AddressLongitude).HasMaxLength(50);

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.LocationLatitude).HasMaxLength(50);

                entity.Property(e => e.LocationLongitude).HasMaxLength(50);

                entity.Property(e => e.StateName).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.ZipCode).HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_UserAddresses_Cities");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_UserAddresses_Countries");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_UserAddresses_States");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAddresses_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users", "dbo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedOnDate).HasColumnType("date");

                entity.Property(e => e.DeletedBy).HasMaxLength(450);

                entity.Property(e => e.DeletedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.StripeCustomerId).HasMaxLength(450);

                entity.Property(e => e.UniqueIdentifier)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
