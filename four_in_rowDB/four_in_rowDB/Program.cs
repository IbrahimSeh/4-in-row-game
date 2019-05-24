using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace four_in_rowDB
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new Four_in_rowContext("four_in_rowDB_amaniIbrahim"))
            {
                ctx.Database.Initialize(force: true);
            }
        }
    }
}
