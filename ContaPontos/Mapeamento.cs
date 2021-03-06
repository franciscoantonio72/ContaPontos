﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ContaPontos
{
    class Mapeamento<T> : IMapeamento<T> where T : class
    {
        public IEnumerable<T> ServicoBusca<T>(string uri)
        {
            WebClient client = new WebClient();
            client.Headers["Accept"] = "application/json";
            client.DownloadStringAsync(new Uri(uri));

            var Retorno = new List<T>();

            client.DownloadStringCompleted += (s1, e1) =>
                {
                    Retorno = JsonConvert.DeserializeObject<List<T>>(e1.Result.ToString());
                };
            return Retorno;
        }
    }
}
