using GalaSoft.MvvmLight.Command;
using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class AddUpdateStatusesViewModel : BaseViewModel
    {
        private Statuses _statuses;
        private Doctor _selectDoctor;
        private Patient _selectPatient;
        private Document _selectDocument;

        public Statuses Statuses
        {
            get => _statuses;
            set
            {
                _statuses = value;
                OnProperty("Statuses");
            }
        }

        public ObservableCollection<Doctor> Doctors
        {
            get => _data.Doctors;
        }

        public ObservableCollection<Patient> Patients
        {
            get => _data.Patients;
        }

        public ObservableCollection<Document> Documents
        {
            get => _data.Documents;
        }

        public Doctor SelectDoctor
        {
            get => _selectDoctor;
            set
            {
                _selectDoctor = value;
                Statuses.DoctorId = value == null ? -1 : value.Id;
                OnProperty("SelectDoctor");
            }
        }

        public Patient SelectPatient
        {
            get => _selectPatient;
            set
            {
                _selectPatient = value;
                Statuses.PatientId = value == null ? -1 : value.Id;
                OnProperty("SelectPatient");
            }
        }

        public Document SelectDocument
        {
            get => _selectDocument;
            set
            {
                _selectDocument = value;
                Statuses.DocumentId = value == null ? -1 : value.Id;
                OnProperty("SelectDocument");
            }
        }

        public AddUpdateStatusesViewModel(DataModel data)
        {
            _data = data;
            Statuses = new Statuses();
            SelectDoctor = _data.Doctors[0];
            SelectDocument = _data.Documents[0];
            SelectPatient = _data.Patients[0];
        }

        public AddUpdateStatusesViewModel(DataModel data, Statuses statuses)
        {
            _data = data;
            Statuses = statuses;
            SelectDoctor = statuses.Doctor;
            SelectDocument = statuses.Document;
            SelectPatient = statuses.Patient;
        }

        public void Add()
        {
            if(Statuses.DisabilityStatus == "")
            {
                Message("Не все поля заполнены");
                return;
            }

            Statuses.Id = _data.Statuses.Count() == 0 ? 1 : _data.Statuses.Last().Id + 1;

            _data.Add(Statuses);

            Close();
        }

        public void Update()
        {
            if (Statuses.DisabilityStatus == "")
            {
                Message("Не все поля заполнены");
                return;
            }

            _data.Update(Statuses);

            Close();
        }

        public RelayCommand AddCommand => new RelayCommand(Add);
        public RelayCommand UpdateCommand => new RelayCommand(Update);
    }
}
