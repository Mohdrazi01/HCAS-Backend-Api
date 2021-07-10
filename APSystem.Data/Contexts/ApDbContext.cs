using System;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using APSystem.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APSystem.Data.Contexts
{
    public class ApDbContext : IdentityDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApDbContext( IHttpContextAccessor httpContextAccessor,DbContextOptions<ApDbContext> options):base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<ApCancellationDbEntity> ApCancellations { get; set; }
        public DbSet<ApFeedbackDbEntity> ApFeedbacks { get; set; }
        public DbSet<AppointmentHistoryDbEntity> AppointmentHistories { get; set; }
        public DbSet<AppointmentsDbEntity> Appointments { get; set; }
        public DbSet<AppointmentStatusDbEntity> AppointmentStatuses { get; set; }
        public DbSet<AppointmentTypeDbEntity> AppointmentType { get; set; }
        public DbSet<ApSlotsDbEntity> ApSlots { get; set; }
        public DbSet<BookingsDbEntity> Bookings { get; set; }
        public DbSet<UsersDbEntity> ApUsers { get; set; }
        public DbSet<RoleDbEntity> ApRoles { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckBeforeSaving();
            return (await base.SaveChangesAsync(true,cancellationToken).ConfigureAwait(false));

        }
        private void CheckBeforeSaving()
        {
           var userId = _httpContextAccessor?.HttpContext.User.FindFirstValue("Id");
            var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).ModifiedDate = DateTime.Now;
                ((BaseEntity)entityEntry.Entity).ModifiedBy = Convert.ToInt32(userId);
                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property("CreatedBy").IsModified = false;
                    entityEntry.Property("CreatedDate").IsModified = false;
                    //entityEntry.Property("IsActive").IsModified = false;
                }
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                    ((BaseEntity)entityEntry.Entity).CreatedBy = Convert.ToInt32(userId);
                    ((BaseEntity)entityEntry.Entity).IsActive = true;
                }
            }
        }
    }
}