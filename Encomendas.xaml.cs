﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Integracao_Windows
{
    /// <summary>
    /// Interaction logic for Encomendas.xaml
    /// </summary>
    public partial class Encomendas : Page
    {
        Modelo context;

        public Encomendas()
        {
            InitializeComponent();
        }

        public Encomendas(Modelo mc) : this()
        {
            context = mc;

            context.Encomendas.Load();
            gridEncomendas.ItemsSource = context.Encomendas.Local;
        }

        public void Update_Data()
        {
        }
    }
}
