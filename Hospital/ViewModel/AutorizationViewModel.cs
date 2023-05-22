using GalaSoft.MvvmLight.Command;
using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class AutorizationViewModel : BaseViewModel
    {
        private Users _users;

        public Action Hide { get; set; }
        public Action Show { get; set; }

        public Users Users
        {
            get => _users;
            set
            {
                _users = value;
                OnProperty("Users");
            }
        }

        public AutorizationViewModel()
        {
            _data = new DataModel();
            Users = new Users();
        }

        public void Check()
        {
            if(Users.Login == "" || Users.Password == "")
            {
                Message("Не все поля заполнены");
                return;
            }

            Users temp = _data.Users.Where(x => x.Login == Users.Login && x.Password == Users.Password).FirstOrDefault();
            if(temp == null)
            {
                Message("Логин и/или пароль неверный");
                return;
            }

            Hide();

            MainWindow main = new MainWindow(_data, temp.RoleId);
            main.ShowDialog();

            Show();
        }

        public RelayCommand CheckCommand => new RelayCommand(Check);
    }
}
