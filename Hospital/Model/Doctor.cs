using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class Doctor : BaseEntity
    {
        public string Surname { get; set; }
        public string Name { get; set; }
    }
}
