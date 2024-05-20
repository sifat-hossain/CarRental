using System.ComponentModel.DataAnnotations;

namespace Campoverde.QMS.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Phone { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
