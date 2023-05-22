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
    /// Логика взаимодействия для AddUserView.xaml
    /// </summary>
    public partial class AddUserView : Window
    {
        public AddUserView(DataModel data)
        {
            InitializeComponent();
            AddUpdateUserViewModel model = new AddUpdateUserViewModel(data);
            DataContext = model;

            if (model.Close == null)
                model.Close = new Action(this.Close);
        }
    }
}
