using Microsoft.EntityFrameworkCore;

namespace PraestoPay.Infrastructure;

public class PraestoPayContext : DbContext
{
    public PraestoPayContext(DbContextOptions<PraestoPayContext> options)
        : base(options)
    {
    }
}
