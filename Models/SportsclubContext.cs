using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SportsClubApi.Models;

public partial class sportsClubContext : DbContext
{
    public sportsClubContext()
    {
    }

    public sportsClubContext(DbContextOptions<sportsClubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<CorporateUser> CorporateUsers { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<MembershipPlan> MembershipPlans { get; set; }

    public virtual DbSet<MembershipPlanAttribute> MembershipPlanAttributes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierActivitiesDetail> SupplierActivitiesDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCategory> UserCategories { get; set; }

    public virtual DbSet<UserConsentDetail> UserConsentDetails { get; set; }

    public virtual DbSet<UserMembershipPlanDetail> UserMembershipPlanDetails { get; set; }

    public virtual DbSet<UserNotificationHistory> UserNotificationHistories { get; set; }

    public virtual DbSet<UserPaymentDetail> UserPaymentDetails { get; set; }

    public virtual DbSet<UserRolesDetail> UserRolesDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=92.204.51.34;Database=sportsclub;User Id=sportsclubuser;Password=98Wuv53d*;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("sportsclubuser");

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("PK__Activiti__482FBD63C117D26D");

            entity.Property(e => e.ActivityId).HasColumnName("activity_id");
            entity.Property(e => e.ActivityDescription)
                .HasColumnType("text")
                .HasColumnName("activity_description");
            entity.Property(e => e.ActivityName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("activity_name");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.CityId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CountryId).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.StateId).HasColumnType("numeric(18, 0)");
        });

        modelBuilder.Entity<CorporateUser>(entity =>
        {
            entity.HasKey(e => e.CorporateId).HasName("PK__Corporat__87E4038688C780C2");

            entity.HasIndex(e => e.CorporateName, "UQ__Corporat__AC5AE83C8B0A88A9").IsUnique();

            entity.Property(e => e.ContactPersonDetail).HasMaxLength(255);
            entity.Property(e => e.ContactPersonEmail).HasMaxLength(255);
            entity.Property(e => e.ContactPersonMobileNo).HasMaxLength(20);
            entity.Property(e => e.ContactPersonName).HasMaxLength(100);
            entity.Property(e => e.CorporateName).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmailIdentifier).HasMaxLength(255);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Isocode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ISOCode");
        });

        modelBuilder.Entity<MembershipPlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Membersh__755C22B73F47B238");

            entity.Property(e => e.CorporateShare).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PlanName).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Corporate).WithMany(p => p.MembershipPlans)
                .HasForeignKey(d => d.CorporateId)
                .HasConstraintName("FK_MembershipPlans_CorporateUsers");
        });

        modelBuilder.Entity<MembershipPlanAttribute>(entity =>
        {
            entity.HasKey(e => e.AttributeId).HasName("PK__Membersh__C18929EA9EE782AE");

            entity.ToTable("MembershipPlanAttribute");

            entity.Property(e => e.Attributedetails).HasMaxLength(255);
            entity.Property(e => e.Attributename).HasMaxLength(100);

            entity.HasOne(d => d.Plan).WithMany(p => p.MembershipPlanAttributes)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK__Membershi__PlanI__41B8C09B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CC5E0ECBC5");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.Property(e => e.StateId)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.StateName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__6EE594E87D1D4C39");

            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.SupplierAddress)
                .HasColumnType("text")
                .HasColumnName("supplier_address");
            entity.Property(e => e.SupplierContact)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("supplier_contact");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("supplier_name");
            entity.Property(e => e.SupplierType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("supplier_type");
        });

        modelBuilder.Entity<SupplierActivitiesDetail>(entity =>
        {
            entity.HasKey(e => e.SupplierActivityId).HasName("PK__Supplier__F5A98DD626C61C7F");

            entity.Property(e => e.SupplierActivityId).HasColumnName("supplier_activity_id");
            entity.Property(e => e.ActivityId).HasColumnName("activity_id");
            entity.Property(e => e.ActivitySchedule)
                .HasColumnType("text")
                .HasColumnName("activity_schedule");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.Activity).WithMany(p => p.SupplierActivitiesDetails)
                .HasForeignKey(d => d.ActivityId)
                .HasConstraintName("FK__SupplierA__activ__45F365D3");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierActivitiesDetails)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__SupplierA__suppl__44FF419A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FF3EB7AF6");

            entity.HasIndex(e => e.Email, "UQ_User_Email")
                .IsUnique()
                .HasFilter("([Email] IS NOT NULL)");

            entity.HasIndex(e => e.Mobile, "UQ_User_Mobile")
                .IsUnique()
                .HasFilter("([Mobile] IS NOT NULL)");

            entity.Property(e => e.AboutMe)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CreatedBy).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.DataVerificationDate).HasColumnType("datetime");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.GoogleLocation)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastModifiedBy).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.LastModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Postalcode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePic).HasColumnType("image");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__UserCate__19093A0BADA6632C");

            entity.ToTable("UserCategory");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserConsentDetail>(entity =>
        {
            entity.HasKey(e => e.ConsentId).HasName("PK__UserCons__E6C2B6785F68E502");

            entity.ToTable("UserConsentDetail");

            entity.Property(e => e.ConsentId).HasColumnName("consent_id");
            entity.Property(e => e.ConsentDate).HasColumnName("consent_date");
            entity.Property(e => e.ConsentStatus).HasColumnName("consent_status");
            entity.Property(e => e.ConsentType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("consent_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserConsentDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserConse__user___4CA06362");
        });

        modelBuilder.Entity<UserMembershipPlanDetail>(entity =>
        {
            entity.HasKey(e => e.UserPlanId).HasName("PK__UserMemb__7E75D75BA3C88419");

            entity.HasOne(d => d.Plan).WithMany(p => p.UserMembershipPlanDetails)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK__UserMembe__plan___4222D4EF");

            entity.HasOne(d => d.User).WithMany(p => p.UserMembershipPlanDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserMembe__user___412EB0B6");
        });

        modelBuilder.Entity<UserNotificationHistory>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__UserNoti__E059842F62741BB6");

            entity.ToTable("UserNotificationHistory");

            entity.Property(e => e.NotificationId).HasColumnName("notification_id");
            entity.Property(e => e.NotificationDate).HasColumnName("notification_date");
            entity.Property(e => e.NotificationMessage)
                .HasColumnType("text")
                .HasColumnName("notification_message");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserNotificationHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserNotif__user___4F7CD00D");
        });

        modelBuilder.Entity<UserPaymentDetail>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__UserPaym__ED1FC9EAF09379C9");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.PaymentDate).HasColumnName("payment_date");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Plan).WithMany(p => p.UserPaymentDetails)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK__UserPayme__plan___49C3F6B7");

            entity.HasOne(d => d.User).WithMany(p => p.UserPaymentDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserPayme__user___48CFD27E");
        });

        modelBuilder.Entity<UserRolesDetail>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__B8D9ABA2FCC71F08");

            entity.ToTable("UserRolesDetail");

            entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRolesDetails)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserRoles__role___3E52440B");

            entity.HasOne(d => d.User).WithMany(p => p.UserRolesDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRoles__user___3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
