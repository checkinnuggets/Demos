using System;
using System.ComponentModel;

namespace ShortlistManager.Services.Models
{
    /*
        Strictly speaking, I'd prefer to have these DisplayName properties on a ViewModel class, 
        but since this is just a bag of properties, I'm happy to push it straight to the browser
        for the purposes of a simple example.
     */

    public class PlayerDto
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Surname")]
        public string Surname { get; set; }

        [DisplayName("Club")]
        public string ClubName { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime? DateOfBirth { get; set; }        
    }
}