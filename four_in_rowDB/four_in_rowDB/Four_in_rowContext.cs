using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace four_in_rowDB
{
    class Four_in_rowContext : DbContext
    {
        public Four_in_rowContext(string databaseName)
            : base(databaseName) { }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}
