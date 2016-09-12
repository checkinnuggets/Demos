using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortlistManager.Services.Db.Entities
{
    [Table("Player")]
    public partial class Player
    {
        public int Id { get; set; }

        // this constraint intentionally omitted from the model validation to demonstrate the
        // handling of the error coming through from the database.
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string Surname { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        public int? ClubId { get; set; }

        public int ScoutId { get; set; }

        public virtual Club Club { get; set; }

        public virtual Scout Scout { get; set; }
    }
}
