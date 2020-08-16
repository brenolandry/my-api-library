using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Models
{
    public class Paginador
    {
        public int pagina { get; set; }
        public int totalPaginas { get; set; }
        public int registroPorPagina { get; set; }
        public int totalRegistros { get; set; }
    }
}
