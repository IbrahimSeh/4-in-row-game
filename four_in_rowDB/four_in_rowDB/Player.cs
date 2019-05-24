using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace four_in_rowDB
{
    class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PlayerId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Points { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
