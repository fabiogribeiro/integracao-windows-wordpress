using System;
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
    /// Interaction logic for Produtos.xaml
    /// </summary>
    public partial class Produtos : Page
    {
        private Modelo context;
        private APIController api = new APIController();

        public Produtos()
        {
            InitializeComponent();
        }

        public Produtos(Modelo mc) : this()
        {
            context = mc;

            context.Produtos.Load();
            lbProdutos.ItemsSource = context.Produtos.Local;
        }

        public async void Update_Data(object sender, EventArgs ea)
        {
            // Obter novos produtos
            var produtos = await api.GetProdutosFrom(context.Produtos.Local.Count());
            context.Produtos.AddRange(produtos);
            context.SaveChanges();
       }

    }
}
