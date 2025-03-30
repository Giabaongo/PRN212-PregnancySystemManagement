using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.Models;

public partial class PregnancyTrackingSystemContext : DbContext
{
    public PregnancyTrackingSystemContext()
    {
    }

    public PregnancyTrackingSystemContext(DbContextOptions<PregnancyTrackingSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Faq> Faqs { get; set; }

    public virtual DbSet<FetalGrowthStandard> FetalGrowthStandards { get; set; }

    public virtual DbSet<FetalMeasurement> FetalMeasurements { get; set; }

    public virtual DbSet<GrowthAlert> GrowthAlerts { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<MembershipPlan> MembershipPlans { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PregnancyProfile> PregnancyProfiles { get; set; }

    public virtual DbSet<Reminder> Reminders { get; set; }

    public virtual DbSet<ScheduledEmail> ScheduledEmails { get; set; }

    public virtual DbSet<User> Users { get; set; }
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Appointments_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasIndex(e => e.PostId, "IX_Comments_PostId");

            entity.HasIndex(e => e.UserId, "IX_Comments_UserId");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments).HasForeignKey(d => d.PostId);

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Faq>(entity =>
        {
            entity.ToTable("FAQs");

            entity.Property(e => e.Category).HasMaxLength(50);
        });

        modelBuilder.Entity<FetalGrowthStandard>(entity =>
        {
            entity.Property(e => e.AbdominalCircumferenceCm).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BiparietalDiameterCm).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FemoralLengthCm).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HeadCircumferenceCm).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HeightCm).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.WeightGrams).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<FetalMeasurement>(entity =>
        {
            entity.HasIndex(e => e.ProfileId, "IX_FetalMeasurements_ProfileId");

            entity.Property(e => e.AbdominalCircumferenceCm).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BiparietalDiameterCm).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FemoralLengthCm).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HeadCircumferenceCm).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HeightCm).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.WeightGrams).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Profile).WithMany(p => p.FetalMeasurements).HasForeignKey(d => d.ProfileId);
        });

        modelBuilder.Entity<GrowthAlert>(entity =>
        {
            entity.HasIndex(e => e.MeasurementId, "IX_GrowthAlerts_MeasurementId");

            entity.HasOne(d => d.Measurement).WithMany(p => p.GrowthAlerts).HasForeignKey(d => d.MeasurementId);
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasIndex(e => e.PlanId, "IX_Memberships_PlanId");

            entity.HasIndex(e => e.UserId, "IX_Memberships_UserId");

            entity.HasOne(d => d.Plan).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.Memberships)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<MembershipPlan>(entity =>
        {
            entity.Property(e => e.PlanName).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasIndex(e => e.DoctorId, "IX_Messages_DoctorId");

            entity.HasIndex(e => e.MemberId, "IX_Messages_MemberId");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MessageDoctors)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Member).WithMany(p => p.MessageMembers)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Notifications_UserId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Message).HasMaxLength(500);

            entity.HasOne(d => d.User).WithMany(p => p.Notifications).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasIndex(e => e.MembershipId, "IX_Payments_MembershipId").IsUnique();

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);

            entity.HasOne(d => d.Membership).WithOne(p => p.Payment)
                .HasForeignKey<Payment>(d => d.MembershipId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Posts_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Posts).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<PregnancyProfile>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_PregnancyProfiles_UserId");

            entity.Property(e => e.PregnancyStatus)
                .HasMaxLength(20)
                .HasComputedColumnSql("(CONVERT([nvarchar](20),case when getdate()<[DueDate] then 'On Going' else 'Completed' end))", false);

            entity.HasOne(d => d.User).WithMany(p => p.PregnancyProfiles).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<ScheduledEmail>(entity =>
        {
            entity.HasIndex(e => e.AppointmentId, "IX_ScheduledEmails_AppointmentId").IsUnique();

            entity.HasOne(d => d.Appointment).WithOne(p => p.ScheduledEmail).HasForeignKey<ScheduledEmail>(d => d.AppointmentId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_Users_Email").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
