using Microsoft.EntityFrameworkCore;

namespace BiteWiseWeb2.Models
{
    public class BiteWiseDb : DbContext
    {
       
            public BiteWiseDb(DbContextOptions<BiteWiseDb> options)
                : base(options)
            {
            }

            public virtual DbSet<User> Users { get; set; }

            // Add other DbSets here if needed
        }
    }

