using ApiAnalyticsApp.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.DataAccess.Helpers
{
    public class AuditableRepository<T> : Repository<T> where T : BaseEntity
    {
        private readonly ApiAnalyticsAppDbContext context;

        public AuditableRepository(ApiAnalyticsAppDbContext context, bool enableSoftDelete = false)
            : base(context)
        {
            this.context = context;
        }

        public async Task SaveAsync(CancellationToken token = default)
        {
            await context.SaveChangesAsync(token).ConfigureAwait(false);
        }
        public async Task InsertRangeAsync(List<T> records, CancellationToken token = default)
        {
            await context.Set<T>().AddRangeAsync(records);
        }
        public void DeleteRange(IEnumerable<T> entities, CancellationToken token = default)
        {
            context.Set<T>().RemoveRange(entities);
        }
        public void UpdateRange(IEnumerable<T> entities, CancellationToken token = default)
        {
            context.Set<T>().UpdateRange(entities);
        }
    }
}
