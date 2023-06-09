﻿using Hospital.ViewModel;
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
    /// Логика взаимодействия для AutorizationView.xaml
    /// </summary>
    public partial class AutorizationView : Window
    {
        public AutorizationView()
        {
            InitializeComponent();
            AutorizationViewModel model = new AutorizationViewModel();
            this.DataContext = model;

            if (model.Hide == null)
                model.Hide = new Action(this.Hide);

            if (model.Show == null)
                model.Show = new Action(this.Show);
        }
    }
}
