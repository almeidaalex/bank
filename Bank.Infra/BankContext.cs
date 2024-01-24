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


        public BankDbContext(DbContextOptions<BankDbContext> options)
          : base(options)
        {
            
        }

        public BankDbContext(DbContextOptions<BankDbContext> options, IMediator mediator)
            :this(options)
        {
            _mediator = mediator;
        }

        public DbSet<Account> Accounts { get; private set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureAccount(modelBuilder);

            ConfigureOwner(modelBuilder);

            ConfigureAccountOperations(modelBuilder);

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

        private static void ConfigureAccountOperations(ModelBuilder builder)
        {
            var operations = builder.Entity<AccountOperation>();
            operations.HasKey(a => a.Id);
            operations.Property(a => a.Id).ValueGeneratedOnAdd();
        }

        private static void ConfigureOwner(ModelBuilder modelBuilder)
        {
            var owner = modelBuilder.Entity<Owner>();
            owner.HasKey(o => o.Id);
            owner.Property(o => o.Id).ValueGeneratedNever();
            owner.Property(o => o.Title).IsRequired();
            //owner.HasMany(o => o.Accounts)
            //     .WithOne(a => a.Owner)
            //     .HasForeignKey(a => a.OwnerId);

            //owner.HasData(new Owner(1, "Alex A."), new Owner(2, "Grace N."));
        }

        private void ConfigureAccount(ModelBuilder builder)
        {
            var account = builder.Entity<Account>();
            account.HasKey(a => a.No);
            account.Property(a => a.No).ValueGeneratedNever();
            account.Property(a => a.OwnerId);

            account.HasMany(a => a.Operations)
                   .WithOne()
                   .HasForeignKey(h => h.AccountNo)
                   .OnDelete(DeleteBehavior.Cascade);

            account.HasOne(a => a.Owner)
                   .WithMany(a => a.Accounts)
                   .HasForeignKey(a => a.OwnerId)
                   .OnDelete(DeleteBehavior.Cascade);


        }

        private static void CleanEvents(IEnumerable<IEntity> entities)
        {
            foreach (var entity in entities)
                entity.ClearEvents();
        }
    }
}
