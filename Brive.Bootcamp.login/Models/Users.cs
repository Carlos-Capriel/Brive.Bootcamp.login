using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brive.Bootcamp.login.Models
{
    [Table("Users")]
    public class Users
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
    }
}
