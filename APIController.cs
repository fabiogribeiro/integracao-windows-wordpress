using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;

namespace Integracao_Windows
{
    class APIController
    {
        private WCObject wc;

        public APIController()
        {
            RestAPI rest = new RestAPI("https://mindboggle-257618.easywp.com/wp-json/wc/v3/", "ck_677e2069693b2a64310671ca9de93c1090f7e294", "cs_b1b0300b985fa3ccf3bbdec4f3d57e3734a05837");
            wc = new WCObject(rest);
        }

        private long DateTimeToUnix(DateTime? dt)
        {
            if (dt.HasValue)
                return (long)(dt.Value.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

            return 0;
        }

        public async Task<List<Encomenda>> GetEncomendasFrom(int offset)
        {
            var orders = await wc.Order.GetAll(new Dictionary<string, string>()
            {
                {"offset", offset.ToString() }
            });

            List<Encomenda> encomendas = new List<Encomenda>();

            orders.ForEach((o) =>
            {
                encomendas.Add(new Encomenda
                {
                    id = (long)o.id,
                    Estado = o.status,
                    Endereco = o.shipping.address_1,
                    DataMod = DateTimeToUnix(o.date_modified_gmt)
                });
            });

            return encomendas;
        }

        public async Task<List<Encomenda>> GetEncomendasFromArray<T>(IEnumerable<T> arr)
        {
            var orders = await wc.Order.GetAll(new Dictionary<string, string>()
            {
                {"include",  string.Join(",", arr)}
            });

            List<Encomenda> encomendas = new List<Encomenda>();

            orders.ForEach((o) =>
            {
                encomendas.Add(new Encomenda
                {
                    id = (long)o.id,
                    Estado = o.status,
                    Endereco = o.shipping.address_1,
                    DataMod = DateTimeToUnix(o.date_modified_gmt)
                });
            });

            return encomendas;
        }

        public async Task<List<Produto>> GetProdutosFrom(int offset)
        {
            var products = await wc.Product.GetAll(new Dictionary<string, string>()
            {
                {"offset", offset.ToString() }
            });

            List<Produto> produtos = new List<Produto>();

            products.ForEach((p) =>
            {
                produtos.Add(new Produto
                {
                    id = (long)p.id,
                    Nome = p.name,
                    Preco = (double)p.price,
                    URLImagem = p.images[0].src,
                    DataMod = DateTimeToUnix(p.date_modified_gmt)
                });
            });

            return produtos;
        }
        public async Task<List<Produto>> GetProdutosFromArray<T>(IEnumerable<T> arr)
        {
            var products = await wc.Product.GetAll(new Dictionary<string, string>()
            {
                {"include",  string.Join(",", arr)}
            });

            List<Produto> produtos = new List<Produto>();

            products.ForEach((p) =>
            {
                produtos.Add(new Produto
                {
                    id = (long)p.id,
                    Nome = p.name,
                    Preco = (double)p.price,
                    URLImagem = p.images[0].src,
                    DataMod = DateTimeToUnix(p.date_modified_gmt)
                });
            });

            return produtos;
        }

    }
}
