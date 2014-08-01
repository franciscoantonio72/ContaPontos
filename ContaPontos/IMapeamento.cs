using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaPontos
{
    interface IMapeamento<T> where T: class
    {
        IEnumerable<T> ServicoBusca<T>(string uri);
    }
}
