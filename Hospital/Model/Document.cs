using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class Document : BaseEntity
    {
        public string Type { get; set; }
        public string Diagnosis { get; set; }
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
    }
}
