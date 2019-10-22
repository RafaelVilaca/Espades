using Espades.Domain.Entities;
using Espades.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Espades.Infrastructure
{
    public class EspadesContext : DbContext
    {
        public EspadesContext(DbContextOptions<EspadesContext> options)
                : base(options)
        {
        }

        public override int SaveChanges()
        {
            using (IDbContextTransaction transaction = base.Database.BeginTransaction())
            {
                int changes = 0;
                try
                {
                    List<EntityEntry> addedEntries = GetAuditChanges(x => x.State == EntityState.Added);
                    changes = base.SaveChanges();
                    base.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                return changes;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            using (IDbContextTransaction transaction = base.Database.BeginTransaction())
            {
                int changes = 0;
                try
                {
                    List<EntityEntry> addedEntries = GetAuditChanges(x => x.State == EntityState.Added);
                    changes = await base.SaveChangesAsync(cancellationToken);
                    await base.SaveChangesAsync(cancellationToken);
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                return changes;
            }
        }

        private List<EntityEntry> GetAuditChanges(Expression<Func<EntityEntry, bool>> expression)
        {
            return ChangeTracker.Entries()
                    .Where(expression.Compile())
                    .Where(p => p.Entity.GetType().Name != "Audit" && p.Entity.GetType().Name != "ConnectionLog")
                    .Distinct()
                    .ToList();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new EnderecoConfiguration(modelBuilder.Entity<Endereco>());
            new PessoaConfiguration(modelBuilder.Entity<Pessoa>());

            foreach (IMutableForeignKey relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
