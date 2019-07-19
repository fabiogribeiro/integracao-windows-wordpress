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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Integracao_Windows
{
    /// <summary>
    /// Interaction logic for Produtos.xaml
    /// </summary>
    public partial class Produtos : Page
    {
        private Modelo context;

        public Produtos()
        {
            InitializeComponent();
        }

        public Produtos(Modelo mc) : this()
        {
            context = mc;
        }

        public void Update_Data()
        {
        }
    }
}
