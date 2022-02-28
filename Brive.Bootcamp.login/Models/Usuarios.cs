using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Brive.Bootcamp.login.Models
{
    [Table("Users")]
    public class Usuarios
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("Nombre")]
        public string Name { get; set; }
        [Column("ApellidoP")]
        public string LastNameP { get; set; }
        [Column("ApellidoM")]
        public string LastNameM { get; set; }
        [Column("FechaNacimiento")]
        public DateTime Birthday { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("Status")]
        public bool Status { get; set; }
        [Column("Created_at")]
        public DateTime Created_at { get; set; }
        [Column("Updated_at")]
        public DateTime Updated_at { get; set; }
    }
}
