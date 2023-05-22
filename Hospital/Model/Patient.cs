using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class Patient : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string DateOfBirth { get; set; } = DateTime.Now.ToShortDateString();
        public string Gender { get; set; }
        public string Adress { get; set; }
        public string Number { get; set; }

        public DateTime Date
        {
            get => DateTime.Parse(DateOfBirth);
            set => DateOfBirth = value.ToShortDateString();
        }
    }
}
