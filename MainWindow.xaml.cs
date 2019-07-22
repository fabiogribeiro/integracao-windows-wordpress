using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Threading;

namespace Integracao_Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Modelo context = new Modelo();
        Encomendas pageEncomendas;
        Produtos pageProdutos;

        public MainWindow()
        {
            InitializeComponent();
            CreateTables();

            pageEncomendas = new Encomendas(context);
            pageProdutos = new Produtos(context);
        }

        private void Encomendas_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(pageEncomendas);
        }


        private void Produtos_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(pageProdutos);
        } 

        private void NovoProduto_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new NovoProduto(pageProdutos));
        }

        /// <summary>
        /// Cria tabelas na base dados de acordo com o modelo, caso estas não existam.
        /// </summary>
        private void CreateTables()
        {
            // Abrir uma ligação temporária
            var cnn = context.Database.Connection;
            cnn.Open();

            // Executar comandos
            var cmProdutos = cnn.CreateCommand();
            cmProdutos.CommandText = "CREATE TABLE IF NOT EXISTS Encomendas ("+
                                        "id         INTEGER NOT NULL UNIQUE," +
                                        "Estado     TEXT," +
	                                    "Endereco   TEXT," +
	                                    "DataMod    INTEGER," +
	                                    "PRIMARY KEY(\"id\")" +
                                     ")";
            cmProdutos.ExecuteNonQuery();

            var cmEncomendas = cnn.CreateCommand();
            cmEncomendas.CommandText = "CREATE TABLE IF NOT EXISTS Produtos ("+
                                        "id         INTEGER NOT NULL UNIQUE," +
                                        "Nome       TEXT," +
	                                    "Preco      REAL," +
	                                    "URLImagem  TEXT," +
	                                    "DataMod    INTEGER," +
	                                    "PRIMARY KEY(\"id\")" +
                                     ")";
            cmEncomendas.ExecuteNonQuery();

            cnn.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitUpdates();

            mainFrame.NavigationService.Navigate(pageProdutos);
        }

        private void InitUpdates()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(pageEncomendas.Update_Data);
            timer.Tick += new EventHandler(pageProdutos.Update_Data);
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
        }
    }
}
