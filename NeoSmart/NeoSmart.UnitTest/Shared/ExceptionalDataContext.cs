using Microsoft.EntityFrameworkCore;
using NeoSmart.Data.Entities;

namespace NeoSmart.UnitTest.Shared
{
    public class ExceptionalDataContext : DataContext
    {
        public ExceptionalDataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new InvalidOperationException("Test Exception");
        }
    }
}