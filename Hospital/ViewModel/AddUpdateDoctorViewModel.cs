using GalaSoft.MvvmLight.Command;
using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class AddUpdateDoctorViewModel : BaseViewModel
    {
        private Doctor _doctor;

        public Doctor Doctor
        {
            get => _doctor;
            set
            {
                _doctor = value;
                OnProperty("Doctor");
            }
        }

        public AddUpdateDoctorViewModel(DataModel data)
        {
            _data = data;
            Doctor = new Doctor();
        }

        public AddUpdateDoctorViewModel(DataModel data, Doctor doctor)
        {
            _data = data;
            Doctor = doctor;
        }

        public void Add()
        {
            if(Doctor.Name == "" || Doctor.Surname == "")
            {
                Message("Не все поля заполнены");
                return;
            }

            Doctor.Id = _data.Doctors.Count() == 0 ? 1 : _data.Doctors.Last().Id + 1;

            _data.Add(Doctor);
            Close();
        }

        public void Update()
        {
            if (Doctor.Name == "" || Doctor.Surname == "")
            {
                Message("Не все поля заполнены");
                return;
            }

            _data.Update(Doctor);
            Close();
        }

        public RelayCommand AddCommand => new RelayCommand(Add);
        public RelayCommand UpdateCommand => new RelayCommand(Update);
    }
}
