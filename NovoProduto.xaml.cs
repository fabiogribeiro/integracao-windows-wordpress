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
    /// Interaction logic for NovoProduto.xaml
    /// </summary>
    public partial class NovoProduto : Page
    {
        private Page pageProdutos;

        public NovoProduto()
        {
            InitializeComponent();
        }

        public NovoProduto(Page voltarProdutos) : this()
        {
            pageProdutos = voltarProdutos;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(pageProdutos);
        }
    }
}
