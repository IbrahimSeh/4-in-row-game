using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace four_in_rowDB
{
    class Game
    {
        public int GameId { get; set; }
        public ICollection<Player> Players { get; set; }
        public string WinnerName { get; set; }
        public int WinnerPoints { get; set; }
        public DateTime TimeOfBeginning { get; set; }
        public DateTime TimeOfEnd { get; set; }
    }
}
