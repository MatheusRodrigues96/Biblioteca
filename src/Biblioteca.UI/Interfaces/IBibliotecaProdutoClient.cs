using Biblioteca.UI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Biblioteca.UI.Interfaces
{
    public interface IBibliotecaProdutoClient
    {
        Task<List<Produto>> GetProductsAsync(string path);
        Task<Produto> GetProductAsync(string path);
        Task<Uri> CreateProductAsync(Produto produto);
        Task<Produto> UpdateProductAsync(Produto produto);
        Task<HttpStatusCode> DeleteProductAsync(string id);
    }
}
