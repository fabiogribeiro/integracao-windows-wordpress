﻿using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;
using System.Linq;

namespace Integracao_Windows
{
    /// <summary>
    /// Classe para interagir com objetos woocommerce e wordpress
    /// e transformá-los nos objetos conhecidos à base de dados.
    /// </summary>
    class APIController
    {
        private WCObject wc;

        public APIController()
        {
            // Utilizamos o segredo aqui no caso de exemplo
            RestAPI rest = new RestAPI("https://mindboggle-257618.easywp.com/wp-json/wc/v3/", "ck_677e2069693b2a64310671ca9de93c1090f7e294", "cs_b1b0300b985fa3ccf3bbdec4f3d57e3734a05837");
            wc = new WCObject(rest);
        }

        private long DateTimeToUnix(DateTime? dt)
        {
            if (dt.HasValue)
                return (long)(dt.Value.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

            return 0;
        }

        /// <summary>
        /// Obter encomendas ignorando as existentes
        /// </summary>
        /// <param name="offset">Numero de encomendas a ignorar</param>
        /// <returns>Lista de encomendas</returns>
        public async Task<List<Encomenda>> GetEncomendasFrom(int offset)
        {
            var orders = await wc.Order.GetAll(new Dictionary<string, string>()
            {
                {"offset", offset.ToString()},
                {"order", "asc" }
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

        /// <summary>
        /// Obter encomendas a partir de lista.
        /// </summary>
        /// <typeparam name="T">Lista em causa</typeparam>
        /// <param name="arr"></param>
        /// <returns>Lista de encomendas</returns>
        public async Task<List<Encomenda>> GetEncomendasFromArray<T>(IEnumerable<T> arr)
        {
            // Se arr estiver vazio já sabemos o resultado
            if (arr.Count() == 0)
                return new List<Encomenda>();

            var orders = await wc.Order.GetAll(new Dictionary<string, string>()
            {
                {"include",  string.Join(",", arr)},
                {"order", "asc" }
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

        /// <summary>
        /// Obter novos produtos a partir de um certo numero.
        /// </summary>
        /// <param name="offset">Numero de produtos a ignorar</param>
        /// <returns>Lista de produtos</returns>
        public async Task<List<Produto>> GetProdutosFrom(int offset)
        {
            var products = await wc.Product.GetAll(new Dictionary<string, string>()
            {
                {"offset", offset.ToString()},
                {"order", "asc" }
            });

            List<Produto> produtos = new List<Produto>();

            products.ForEach((p) =>
            {
                Produto newp = new Produto
                {
                    id = (long)p.id,
                    Nome = p.name,
                    Preco = (double)p.price,
                    DataMod = DateTimeToUnix(p.date_modified_gmt)
                };

                if (p.images.Count > 0)
                    newp.URLImagem = p.images[0].src;

                produtos.Add(newp);
            });

            return produtos;
        }

        public async Task<Produto> CreateProduct(string name, decimal price, string desc, string image)
        {
            Product p = new Product
            {
                name = name,
                regular_price = price,
                description = desc
            };

            if (!string.IsNullOrWhiteSpace(image))
            {
                List<ProductImage> images = new List<ProductImage>();
                images.Add(new ProductImage
                {
                    src = image
                });

                p.images = images;
            }

            Product response = await wc.Product.Add(p);

            Produto result = new Produto
            {
                id = (long)response.id,
                Nome = response.name,
                Preco = (double)response.price,
            };

            if (response.images.Count > 0)
                result.URLImagem = response.images[0].src;

            return result;
        }
    }
}
