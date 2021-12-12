using Biblioteca.UI.Interfaces;
using Biblioteca.UI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Biblioteca.UI.Services
{
    public class BibliotecaProdutoClient : IBibliotecaProdutoClient
    {
        static HttpClient _client;
        public BibliotecaProdutoClient()
        {
            _client = new HttpClient();
            ConfigureHttpClient();
        }

        static void ConfigureHttpClient()
        {
            _client.BaseAddress = new Uri("https://localhost:44396/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Uri> CreateProductAsync(Produto produto)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(
                "api/Produtos", produto);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        public async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(
                $"api/Produtos/{id}");
            return response.StatusCode;
        }

        public async Task<Produto> GetProductAsync(string path)
        {
            Produto product = null;
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadFromJsonAsync<Produto>();
            }
            return product;
        }

        public async Task<List<Produto>> GetProductsAsync(string path)
        {
            List<Produto> product = null;
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadFromJsonAsync<List<Produto>>();
            }
            return product;
        }

        public async Task<Produto> UpdateProductAsync(Produto produto)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(
                $"api/Produtos/{produto.Id}", produto);
            response.EnsureSuccessStatusCode();

            produto = await response.Content.ReadFromJsonAsync<Produto>();
            return produto;
        }
    }
}
