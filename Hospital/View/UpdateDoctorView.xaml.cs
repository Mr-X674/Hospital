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
    /// Логика взаимодействия для UpdateDoctorView.xaml
    /// </summary>
    public partial class UpdateDoctorView : Window
    {
        public UpdateDoctorView(DataModel data, Doctor doctor)
        {
            InitializeComponent();
            AddUpdateDoctorViewModel model = new AddUpdateDoctorViewModel(data, doctor);
            this.DataContext = model;

            if (model.Close == null)
                model.Close = new Action(this.Close);
        }
    }
}
