using GalaSoft.MvvmLight.Command;
using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class AddUpdatePatientViewModel : BaseViewModel
    {
        private Patient _patient;
        private List<string> _gender = new List<string>()
        {
            "Мужской",
            "Женский"
        };

        private string _selectGender;

        public Patient Patient
        {
            get => _patient;
            set
            {
                _patient = value;
                OnProperty("Patient");
            }
        }

        public List<string> Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnProperty("Gender");
            }
        }

        public string SelectGender
        {
            get => _selectGender;
            set
            {
                _selectGender = value;
                Patient.Gender = value == null ? "" : value;
                OnProperty("SelectGender");
            }
        }

        public AddUpdatePatientViewModel(DataModel data)
        {
            _data = data;
            Patient = new Patient();
            SelectGender = Gender[0];
        }

        public AddUpdatePatientViewModel(DataModel data, Patient patient)
        {
            _data = data;
            Patient = patient;
            SelectGender = patient.Gender;
        }

        public void Add()
        {
            if(Patient.Surname == "" || Patient.Name == "" || Patient.Patronymic == "" || Patient.Adress == "" || Patient.Number == "")
            {
                Message("Не все поля заполнены");
                return;
            }

            Patient.Id = _data.Patients.Count() == 0 ? 1 : _data.Patients.Last().Id + 1;
            _data.Add(Patient);

            Close();
        }

        public void Update()
        {
            if (Patient.Surname == "" || Patient.Name == "" || Patient.Patronymic == "" || Patient.Adress == "" || Patient.Number == "")
            {
                Message("Не все поля заполнены");
                return;
            }

            _data.Update(Patient);

            Close();
        }

        public RelayCommand AddCommand => new RelayCommand(Add);
        public RelayCommand UpdateCommand => new RelayCommand(Update);
    }
}
