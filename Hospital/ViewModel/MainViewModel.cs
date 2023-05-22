using GalaSoft.MvvmLight.Command;
using Hospital.Model;
using Hospital.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private string _find;

        #region private Collection
        private ObservableCollection<Doctor> _doctors { get; set; }
        private ObservableCollection<Document> _documents { get; set; }
        private ObservableCollection<Patient> _patients { get; set; }
        private ObservableCollection<Statuses> _statuses { get; set; }
        private ObservableCollection<Users> _users { get; set; }
        private ObservableCollection<Statuses> _findStatuses { get; set; }
        #endregion

        #region private Select
        private Doctor _selectDoctor { get; set; }
        private Document _selectDocument { get; set; }
        private Patient _selectPatient { get; set; }
        private Statuses _selectStatuses { get; set; }
        public Users _selectUsers { get; set; }
        #endregion

        #region public Collection
        public ObservableCollection<Doctor> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value;
                OnProperty("Doctors");
            }
        }
        public ObservableCollection<Document> Documents
        {
            get => _documents;
            set
            {
                _documents = value;
                OnProperty("Documents");
            }
        }
        public ObservableCollection<Patient> Patients
        {
            get => _patients;
            set
            {
                _patients = value;
                OnProperty("Patients");
            }
        }
        public ObservableCollection<Statuses> Statuses
        {
            get => _statuses;
            set
            {
                _statuses = value;
                OnProperty("Statuses");
            }
        }
        public ObservableCollection<Users> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnProperty("Users");
            }
        }
        public ObservableCollection<Statuses> FindStatuses
        {
            get => _findStatuses;
            set
            {
                _findStatuses = value;
                OnProperty("FindStatuses");
            }
        }
        #endregion

        #region public Select
        public Doctor SelectDoctor
        {
            get => _selectDoctor;
            set
            {
                _selectDoctor = value;
                OnProperty("SelectDoctor");
            }
        }
        public Document SelectDocument
        {
            get => _selectDocument;
            set
            {
                _selectDocument = value;
                OnProperty("SelectDocument");
            }
        }
        public Patient SelectPatient
        {
            get => _selectPatient;
            set
            {
                _selectPatient = value;
                OnProperty("SelectPatient");
            }
        }
        public Statuses SelectStatuses
        {
            get => _selectStatuses;
            set
            {
                _selectStatuses = value;
                OnProperty("SelectStatuses");
            }
        }
        public Users SelectUsers
        {
            get => _selectUsers;
            set
            {
                _selectUsers = value;
                OnProperty("SelectUsers");
            }
        }
        #endregion

        #region Visibility
        private Visibility _isAdmin = Visibility.Visible;
        private Visibility _isUser = Visibility.Visible;
        public Visibility IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnProperty("IsAdmin");
            }
        }

        public Visibility IsUser
        {
            get => _isUser;
            set => _isUser = value;
        }
        #endregion

        public string Find
        {
            get => _find;
            set
            {
                _find = value;
                FindStatuses = new ObservableCollection<Statuses>(_data.Statuses.Where(x => x.Patient.Surname.Contains(value)).ToList());
                OnProperty("Find");
            }
        }

        public MainViewModel(DataModel data, int roleID)
        {
            _data = data;

            Doctors = _data.Doctors;
            Documents = _data.Documents;
            Patients = _data.Patients;
            Statuses = _data.Statuses;
            Users = _data.Users;
            FindStatuses = _data.Statuses;

            switch(roleID)
            {
                case 1:
                    {
                        IsAdmin = Visibility.Visible;
                        IsUser = Visibility.Collapsed;
                    };break;
                case 2:
                    {
                        IsAdmin = Visibility.Collapsed;
                        IsUser = Visibility.Visible;
                    };break;
            }

        }

        #region Doctor
        private void AddDoctors()
        {
            AddDoctorView add = new AddDoctorView(_data);
            add.ShowDialog();

            Doctors = _data.Doctors;
        }

        private void UpdateDoctors()
        {
            if(SelectDoctor == null)
            {
                Message("Не выбран доктор");
                return;
            }

            UpdateDoctorView update = new UpdateDoctorView(_data, SelectDoctor);
            update.ShowDialog();

            Doctors = _data.Doctors;
            Statuses = _data.Statuses;
            Users = _data.Users;
        }

        private void RemoveDoctors()
        {
            if (SelectDoctor == null)
            {
                Message("Не выбран доктор");
                return;
            }

            List<Users> users = _data.Users.Where(x => x.DoctorId == SelectDoctor.Id).ToList();
            users.ForEach(x => _data.Remove(x));

            List<Statuses> statuses = _data.Statuses.Where(x => x.DoctorId == SelectDoctor.Id).ToList();
            statuses.ForEach(x => x.DoctorId = null);
            statuses.ForEach(x => _data.Update(x));

            _data.Remove(SelectDoctor);

            Users = _data.Users;
            Doctors = _data.Doctors;
            Statuses = _data.Statuses;
        }
        #endregion

        #region Documents
        private void AddDocument()
        {
            AddDocumentView add = new AddDocumentView(_data);
            add.ShowDialog();

            Documents = _data.Documents;
        }

        private void UpdateDocument()
        {
            if(SelectDocument == null)
            {
                Message("");
                return;
            }

            UpdateDocumentView update = new UpdateDocumentView(_data, SelectDocument);
            update.ShowDialog();

            Documents = _data.Documents;
            Statuses = _data.Statuses;
        }

        private void RemoveDocument()
        {
            if (SelectDocument == null)
            {
                Message("");
                return;
            }

            List<Statuses> statuses = _data.Statuses.Where(x => x.DocumentId == SelectDocument.Id).ToList();
            statuses.ForEach(x => x.DocumentId = null);
            statuses.ForEach(x => _data.Update(x));

            _data.Remove(SelectDocument);

            Documents = _data.Documents;
            Statuses = _data.Statuses;
        }
        #endregion

        #region Patient
        private void AddPatient()
        {
            AddPatientView add = new AddPatientView(_data);
            add.ShowDialog();

            Patients = _data.Patients;
        }

        private void UpdatePatient()
        {
            if(SelectPatient == null)
            {
                Message("Поциент не выбран");
                return;
            }

            UpdatePatientView update = new UpdatePatientView(_data, SelectPatient);
            update.ShowDialog();

            Patients = _data.Patients;
            Statuses = _data.Statuses;
        }

        private void RemovePatient()
        {
            if (SelectPatient == null)
            {
                Message("Поциент не выбран");
                return;
            }

            List<Statuses> statuses = _data.Statuses.Where(x => x.PatientId == SelectPatient.Id).ToList();
            statuses.ForEach(x => x.DocumentId = null);
            statuses.ForEach(x => _data.Update(x));

            _data.Remove(SelectPatient);

            Patients = _data.Patients;
            Statuses = _data.Statuses;
        }
        #endregion

        #region Statuses
        private void AddStatuses()
        {
            AddStatusesView add = new AddStatusesView(_data);
            add.ShowDialog();

            Statuses = _data.Statuses;
        }

        private void UpdateStatuses()
        {
            if(SelectStatuses == null)
            {
                Message("Статус не выбран");
                return;
            }

            if (SelectStatuses.DoctorId == null || SelectStatuses.DocumentId == null || SelectStatuses.PatientId == null)
            {
                Message("Обновление данных невозможно");
                return;
            }

            UpdateStatusesView update = new UpdateStatusesView(_data, SelectStatuses);
            update.ShowDialog();

            Statuses = _data.Statuses;
        }

        private void RemoveStatuses()
        {
            if(SelectStatuses == null)
            {
                Message("Статус не выбран");
                return;
            }

            _data.Remove(SelectStatuses);

            Statuses = _data.Statuses;
        }
        #endregion

        #region Users
        private void AddUsers()
        {
            AddUserView add = new AddUserView(_data);
            add.ShowDialog();

            Users = _data.Users;
        }

        private void UpdateUsers()
        {
            if(SelectUsers == null)
            {
                Message("Пользователь не выбран");
                return;
            }

            UpdateUserView update = new UpdateUserView(_data, SelectUsers);
            update.ShowDialog();

            Users = _data.Users;
        }

        private void RemoveUsers()
        {
            if (SelectUsers == null)
            {
                Message("Пользователь не выбран");
                return;
            }

            _data.Remove(SelectUsers);
        }
        #endregion

        public RelayCommand AddDoctorsCommand => new RelayCommand(AddDoctors);
        public RelayCommand UpdateDoctorsCommand => new RelayCommand(UpdateDoctors);
        public RelayCommand RemoveDoctorsCommand => new RelayCommand(RemoveDoctors);

        public RelayCommand AddDocumentsCommand => new RelayCommand(AddDocument);
        public RelayCommand UpdateDocumentsCommand => new RelayCommand(UpdateDocument);
        public RelayCommand RemoveDocumentsCommand => new RelayCommand(RemoveDocument);

        public RelayCommand AddPatientsCommand => new RelayCommand(AddPatient);
        public RelayCommand UpdatePatientsCommand => new RelayCommand(UpdatePatient);
        public RelayCommand RemovePatientsCommand => new RelayCommand(RemovePatient);

        public RelayCommand AddStatusesCommand => new RelayCommand(AddStatuses);
        public RelayCommand UpdateStatusesCommand => new RelayCommand(UpdateStatuses);
        public RelayCommand RemoveStatusesCommand => new RelayCommand(RemoveStatuses);

        public RelayCommand AddUsersCommand => new RelayCommand(AddUsers);
        public RelayCommand UpdateUsersCommand => new RelayCommand(UpdateUsers);
        public RelayCommand RemoveUsersCommand => new RelayCommand(RemoveUsers);

    }
}
