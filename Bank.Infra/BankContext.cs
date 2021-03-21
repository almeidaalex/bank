using Bank.Domain;
using Bank.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections;
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
            modelBuilder.Entity<Account>().HasKey(a => a.No);
            modelBuilder.Entity<AccountHistory>().HasKey(a => a.Id);
            modelBuilder.Entity<AccountHistory>().Property(a => a.Id).ValueGeneratedOnAdd();

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
