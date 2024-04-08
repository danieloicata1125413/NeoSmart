using Microsoft.EntityFrameworkCore;
using NeoSmart.Data.Entities;

namespace NeoSmart.UnitTest.Shared
{
    public class ExceptionalDBUpdateDataContext : DataContext
    {
        public ExceptionalDBUpdateDataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new DbUpdateException("Test Exception");
        }
    }
}