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
    public class AddUpdateUserViewModel : BaseViewModel
    {
        private Users _user;
        private Doctor _selectDoctor;
        private Roles _selectRoles;

        public Users User
        {
            get => _user;
            set
            {
                _user = value;
                OnProperty("User");
            }
        }

        public ObservableCollection<Doctor> Doctors
        {
            get => _data.Doctors;
        }

        public ObservableCollection<Roles> Roles
        {
            get => _data.Roles;
        }
        public Doctor SelectDoctor
        {
            get => _selectDoctor;
            set
            {
                _selectDoctor = value;
                User.DoctorId = value == null ? -1 : value.Id;
                OnProperty("SelectDoctor");
            }
        }

        public Roles SelectRoles
        {
            get => _selectRoles;
            set
            {
                _selectRoles = value;
                User.RoleId = value == null ? 1 : value.Id;
                OnProperty("SelectRoles");
            }
        }

        public AddUpdateUserViewModel(DataModel data)
        {
            _data = data;
            User = new Users();
            SelectDoctor = _data.Doctors[0];
            SelectRoles = _data.Roles[0];
        }

        public AddUpdateUserViewModel(DataModel data, Users users)
        {
            _data = data;
            User = users;
            SelectDoctor = users.Doctor;
            SelectRoles = users.Roles;
        }

        private void Add()
        {
            if(User.Login == "" || User.Password == "")
            {
                Message("");
                return;
            }

            User.Id = _data.Users.Count() == 0 ? 1 : _data.Users.Last().Id + 1;

            _data.Add(User);
            Close();
        }

        private void Update()
        {
            if (User.Login == "" || User.Password == "")
            {
                Message("");
                return;
            }

            _data.Update(User);
            Close();
        }

        public RelayCommand AddCommand => new RelayCommand(Add);
        public RelayCommand UpdateCommand => new RelayCommand(Update);
    }
}
