using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class Statuses : BaseEntity
    {
        public int? DoctorId { get; set; } = null;
        public int? PatientId { get; set; } = null;
        public int? DocumentId { get; set; }
        public string DisabilityStatus { get; set; }
        public string DateOfIssue { get; set; } = DateTime.Now.ToShortDateString();
        public string DateEnd { get; set; } = DateTime.Now.ToShortDateString();

        public DateTime DateStart
        {
            get => DateTime.Parse(DateOfIssue);
            set => DateOfIssue = value.ToShortDateString();
        }

        public DateTime DateEnd2
        {
            get => DateTime.Parse(DateEnd);
            set => DateEnd = value.ToShortDateString();
        }

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public Document Document { get; set; }
    }
}
