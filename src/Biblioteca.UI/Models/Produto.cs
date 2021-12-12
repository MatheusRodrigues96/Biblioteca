namespace Biblioteca.UI.Models
{
    public class Produto
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Editora { get; set; }
        public string TipoProduto { get; set; }
    }
}
