using Hospital.Model;
using Hospital.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Логика взаимодействия для UpdateUserView.xaml
    /// </summary>
    public partial class UpdateUserView : Window
    {
        public UpdateUserView(DataModel data, Users user)
        {
            InitializeComponent();
            AddUpdateUserViewModel model = new AddUpdateUserViewModel(data, user);
            DataContext = model;

            if (model.Close == null)
                model.Close = new Action(this.Close);
        }
    }
}
