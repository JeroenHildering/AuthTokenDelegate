using System.ComponentModel.DataAnnotations;

namespace AuthTokenPropagate.Models
{
    public class TokenRequest
    {
        [Required]
        public string UserName { get; set; }
    }
}
