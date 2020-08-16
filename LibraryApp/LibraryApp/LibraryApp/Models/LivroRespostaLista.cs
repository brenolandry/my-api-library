using System.Collections.Generic;

namespace LibraryApp.Models
{
    public class LivroRespostaLista
    {
        public List<Livro> lista { get; set; }
        public Paginador paginador { get; set; }
    }
}
