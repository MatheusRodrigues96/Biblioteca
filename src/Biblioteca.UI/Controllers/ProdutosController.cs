using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca.UI.Models;
using Biblioteca.UI.Interfaces;

namespace Biblioteca.UI.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IBibliotecaProdutoClient _produtoClient;

        public ProdutosController(IBibliotecaProdutoClient client)
        {
            _produtoClient = client;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            
            return View(await _produtoClient.GetProductsAsync("api/Produtos"));
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _produtoClient.GetProductAsync($"api/Produtos/{id}");
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Autor,Editora,TipoProduto")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                await _produtoClient.CreateProductAsync(produto);
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _produtoClient.GetProductAsync($"api/Produtos/{id}");
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Titulo,Autor,Editora,TipoProduto")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _produtoClient.UpdateProductAsync(produto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = _produtoClient.GetProductAsync($"api/Produtos/{id}");
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _produtoClient.DeleteProductAsync(id.ToString());
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(long id)
        {
            var produto = _produtoClient.GetProductAsync($"api/Produtos/{id}");
            if (produto == null)
            {
                return false;
            }
            return true;
        }
    }
}
