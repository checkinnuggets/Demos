using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortlistManager.Services.Db.Entities
{
    [Table("Club")]
    public partial class Club
    {
        public Club()
        {
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
