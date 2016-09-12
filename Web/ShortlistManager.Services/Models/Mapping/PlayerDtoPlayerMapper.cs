using ShortlistManager.Services.Db.Entities;

namespace ShortlistManager.Services.Models.Mapping
{
    public class PlayerDtoPlayerMapper : BaseMapper<Player, PlayerDto>
    {
        public override void Update(PlayerDto dest, Player src)
        {
            dest.Id = src.Id;
            dest.FirstName = src.FirstName;
            dest.Surname = src.Surname;
            dest.DateOfBirth = src.DateOfBirth;
            dest.ClubName = src.Club?.Name;
        }
    }
}
