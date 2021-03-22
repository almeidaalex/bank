using Bank.Domain;
using Bank.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Infra
{
    public class BankDbContext: DbContext
    {
        private readonly IMediator _mediator;

        public BankDbContext(DbContextOptions<BankDbContext> options, IMediator mediator)
            :base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Account> Accounts { get; private set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var owner = modelBuilder.Entity<Owner>();
            owner.HasKey(o => o.Id);            

            var account = modelBuilder.Entity<Account>();
            account.HasKey(a => a.No);
            account.HasMany(a => a.History)
                   .WithOne()
                   .HasForeignKey(h => h.AccountNo);

            account.HasOne(a => a.Owner)
                   .WithMany()
                   .HasForeignKey(a => a.OwnerId);

            var history = modelBuilder.Entity<AccountHistory>();
            history.HasKey(a => a.Id);
            history.Property(a => a.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entities = this.ChangeTracker.Entries<IEntity>()
                .Select(po => po.Entity)
                .Where(po => po.Events.Any())
                .ToArray();

            var domainEvents = entities.SelectMany(e => e.Events).ToArray();

            CleanEvents(entities);

            foreach (var @event in domainEvents)
                _mediator.Publish(@event, cancellationToken);

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private static void CleanEvents(IEnumerable<IEntity> entities)
        {
            foreach (var entity in entities)            
                entity.ClearEvents();            
        }

        
    }
}
