using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class Users : BaseEntity
    {
        public int RoleId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }
        public Roles Roles { get; set; }
    }
}
