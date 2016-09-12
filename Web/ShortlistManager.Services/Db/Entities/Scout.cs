using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortlistManager.Services.Db.Entities
{
    [Table("Scout")]
    public partial class Scout
    {
        public Scout()
        {
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
