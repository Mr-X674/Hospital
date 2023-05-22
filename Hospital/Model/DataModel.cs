using Hospital.Model.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class DataModel
    {
        private ChangeDB _changeDB;

        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Document> Documents { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Roles> Roles { get; set; }
        public ObservableCollection<Statuses> Statuses { get; set; }
        public ObservableCollection<Users> Users { get; set; }

        public DataModel()
        {
            _changeDB = new ChangeDB();
            Initial();
        }

        public void Initial()
        {
            Doctors = _changeDB.GetDoctor();
            Documents = _changeDB.GetDocuments();
            Patients = _changeDB.GetPatients();
            Roles = _changeDB.GetRoles();
            Statuses = _changeDB.GetStatuses(); 
            Users = _changeDB.GetUsers();

            Statuses.ToList().ForEach(x => x.DocumentId = x.DocumentId == -1 ? null : x.DocumentId);
            Statuses.ToList().ForEach(x => x.PatientId = x.PatientId == -1 ? null : x.PatientId);
            Statuses.ToList().ForEach(x => x.DoctorId = x.DoctorId == -1 ? null : x.DoctorId);

            Statuses.ToList().ForEach(x => x.Document = x.DocumentId != null ? Documents.Where(y => y.Id == x.DocumentId).FirstOrDefault() : null);
            Statuses.ToList().ForEach(x => x.Doctor = x.DoctorId != null ? Doctors.Where(y => y.Id == x.DoctorId).FirstOrDefault() : null);
            Statuses.ToList().ForEach(x => x.Patient = x.PatientId != null ? Patients.Where(y => y.Id == x.PatientId).FirstOrDefault() : null);

            Users.ToList().ForEach(x => x.Roles = Roles.Where(y => y.Id == x.RoleId).FirstOrDefault());
            Users.ToList().ForEach(x => x.Doctor = Doctors.Where(y => y.Id == x.DoctorId).FirstOrDefault());
        }

        public void Add(BaseEntity baseEntity)
        {
            _changeDB.AddAll(baseEntity);
            Initial();
        }

        public void Update(BaseEntity baseEntity)
        {
            _changeDB.UpdateAll(baseEntity);
            Initial();
        }

        public void Remove(BaseEntity baseEntity)
        {
            _changeDB.RemoveAll(baseEntity); 
            Initial();
        }
    }
}
