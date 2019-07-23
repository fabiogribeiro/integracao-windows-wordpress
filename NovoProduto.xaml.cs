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
        private Modelo context;
        private Page pageProdutos;
        private APIController api = new APIController();

        public NovoProduto()
        {
            InitializeComponent();
        }

        public NovoProduto(Modelo mc, Page voltarProdutos) : this()
        {
            context = mc; 
            pageProdutos = voltarProdutos;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNome.Text) || string.IsNullOrWhiteSpace(tbDescricao.Text))
            {
                MessageBox.Show("Preencha todos os campos.", "Erro");
                return;
            }

            decimal price;
            if(decimal.TryParse(tbPreco.Text, out price))
            {
                try
                {
                    Produto produto = await api.CreateProduct(tbNome.Text, price, tbDescricao.Text, tbURL.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message, "Erro");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Introduza um inteiro ou decimal (separado por vírgula ou outro dependendo da zona).", "Erro");
                return;
            }

            NavigationService.Navigate(pageProdutos);
        }
    }
}
