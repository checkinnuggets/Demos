using System;
using FluentValidation;
using ShortlistManager.Services.Models;

namespace ShortlistManager.Web.Infrastructure.Validation
{
    /*
     * The thing I like about FluentValidation is that it decouples this validation logic
     * from the PlayerDto object itself.
     */ 
    public class PlayerValidator : AbstractValidator<PlayerDto>
    {
        private const byte MinPlayerAge = 16;
        private const byte MaxPlayerAge = 42;

        public PlayerValidator()
        {
            RuleFor(player => player.Id)
                .NotNull();

            RuleFor(player => player.FirstName)
                .Length(0, 32);

            RuleFor(player => player.Surname)
                .Length(0, 64)
                .NotNull();

            RuleFor(player => player.ClubName)
                .Length(0, 64);

            RuleFor(player => player.DateOfBirth)
                .InclusiveBetween(DateTime.Today.AddYears(0 - MaxPlayerAge), DateTime.Today.AddYears(0 - MinPlayerAge));
        }

    }
}