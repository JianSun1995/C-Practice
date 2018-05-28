using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DesktopContactApp.Classes
{
    public class Contact
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}  Name:{Name} Emil: {Email}  Phone Number: {PhoneNumber}";
        }
    }
}
