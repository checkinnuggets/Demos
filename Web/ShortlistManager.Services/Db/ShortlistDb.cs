using System.Data.Entity;
using ShortlistManager.Services.Db.Entities;

namespace ShortlistManager.Services.Db
{
    public partial class ShortlistDb : DbContext, IDbContext
    {
        public ShortlistDb()
            : base("name=ShortlistDb")
        {
        }

        public virtual IDbSet<Club> Clubs { get; set; }

        public virtual IDbSet<Player> Players { get; set; }
        public virtual IDbSet<Scout> Scouts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scout>()
                .HasKey(x=>x.Id)
                .HasMany(e => e.Players)
                .WithRequired(e => e.Scout)
                .WillCascadeOnDelete(false);
        }

        // for the purposes of satisfying the interface
        public new void SaveChanges() { base.SaveChanges(); }
    }
}
