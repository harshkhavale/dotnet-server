using Microsoft.EntityFrameworkCore;
using SportsClubApi.Models;

public class SportsClubContext : DbContext
{
    public SportsClubContext(DbContextOptions<SportsClubContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<CorporateUser> CorporateUsers { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<MembershipPlan> MembershipPlans { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Countries> Countries { get; set; }
    public DbSet<States> States { get; set; }
    public DbSet<Cities> Cities { get; set; }
    public DbSet<UserRolesDetail> UserRolesDetails { get; set; }
    public DbSet<UserMembershipPlanDetail> UserMembershipPlanDetails { get; set; }
    public DbSet<SupplierActivitiesDetail> SupplierActivitiesDetails { get; set; }
    public DbSet<UserPaymentDetail> UserPaymentDetails { get; set; }
    public DbSet<UserConsentDetail> UserConsentDetails { get; set; }
    public DbSet<UserNotificationHistory> UserNotificationHistories { get; set; }
    public DbSet<MembershipPlanAttribute> MembershipPlanAttribute { get; set; }
    public DbSet<UserCategory> UserCategory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<Role>()
            .HasKey(r => r.RoleId);

        modelBuilder.Entity<CorporateUser>()
            .HasKey(cu => cu.CorporateId);

        modelBuilder.Entity<Supplier>()
            .HasKey(s => s.SupplierId);

        modelBuilder.Entity<MembershipPlan>()
            .HasKey(mp => mp.PlanId);

        modelBuilder.Entity<Activity>()
            .HasKey(a => a.ActivityId);

        modelBuilder.Entity<UserRolesDetail>()
            .HasKey(urd => urd.UserRoleId);

        modelBuilder.Entity<UserMembershipPlanDetail>()
            .HasKey(umpd => umpd.UserPlanId);

        modelBuilder.Entity<SupplierActivitiesDetail>()
            .HasKey(sad => sad.SupplierActivityId);

        modelBuilder.Entity<UserPaymentDetail>()
            .HasKey(upd => upd.PaymentId);

        modelBuilder.Entity<UserConsentDetail>()
            .HasKey(ucd => ucd.ConsentId);

        modelBuilder.Entity<UserNotificationHistory>()
            .HasKey(unh => unh.NotificationId);

        modelBuilder.Entity<MembershipPlan>()
            .Property(mp => mp.Price)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Countries>()
            .HasKey(c => c.CountryId);



        modelBuilder.Entity<States>()
            .HasKey(s => s.StateId);




        modelBuilder.Entity<Cities>()
        
           .HasKey(s => s.CityId);


        modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Mobile)
            .IsUnique();

        modelBuilder.Entity<MembershipPlanAttribute>()
               .HasKey(mpa => mpa.AttributeId);
        modelBuilder.Entity<UserCategory>()
                .HasKey(uc => uc.CategoryId);



    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<CorporateUser>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDateTime = DateTime.UtcNow;
                entry.Entity.ModifiedDateTime = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedDateTime = DateTime.UtcNow;

                if (entry.Entity.CreatedDateTime < (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue)
                {
                    entry.Entity.CreatedDateTime = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                }
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

}
