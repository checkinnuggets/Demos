using System.ComponentModel.DataAnnotations;

namespace ShortlistManager.Web.Models
{
    public class LogInModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}