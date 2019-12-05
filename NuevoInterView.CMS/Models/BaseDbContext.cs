using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NuevoInterView.CMS.Models
{
    public class BaseDbContext : DbContext
    {
        public DbSet<PingControl> PingControls { get; set; }

        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        {
        }



    }

}
