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
    /// Interaction logic for Encomendas.xaml
    /// </summary>
    public partial class Encomendas : Page
    {
        Modelo context;
        APIController api = new APIController();

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

        public async void Update_Data(object sender, EventArgs ea)
        {
            // Atualizar encomendas na base de dados caso necessário
            var encomendas = context.Encomendas.Local;
            var apiEncomendas = await api.GetEncomendasFromArray(encomendas.Select((e) => e.id));

            for (int i = 0; i < encomendas.Count; ++i)
            {
                // Atualizamos se modificado
                if (apiEncomendas[i].DataMod > encomendas[i].DataMod)
                {
                    encomendas[i].Estado = apiEncomendas[i].Estado;
                    encomendas[i].Endereco = apiEncomendas[i].Endereco;
                    encomendas[i].DataMod = apiEncomendas[i].DataMod;
                    context.SaveChanges();

                    // Estas alterações não notificam a lista por isso atualizamos
                    gridEncomendas.Items.Refresh();

                }
            }

            // Obter novas encomendas
            var recentEncomendas = await api.GetEncomendasFrom(encomendas.Count());
            context.Encomendas.AddRange(recentEncomendas);
            context.SaveChanges();
       }
    }
}
