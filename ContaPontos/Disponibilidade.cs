using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaPontos
{
    class Disponibilidade
    {
        DateTime Checkin {get; set;}
        DateTime Checkout {get; set;}
        int QuantidadeAdultos {get; set;}
        int QuantidadeCriancas {get; set;}
        string Status {get; set;}
        string Motivo {get; set;}
        TiposApartamentos[] tiposApartamentos { get; set; }
    }
}
