using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class LivroRespostaLista
    {
        List<Livro> lista { get; set; }
        Paginador paginador { get; set; }
    }
}
