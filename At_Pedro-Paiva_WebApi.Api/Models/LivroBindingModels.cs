using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace At_Pedro_Paiva_WebApi.Api.Models
{
    public class LivroBindingModels
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public DateTime Ano { get; set; }
        public IEnumerable<int> AutorId { get; set; }
        public IEnumerable<string> AutorNome { get; set; }
    }

    public class LivroCreateBindingModels
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public DateTime Ano { get; set; }
        public int AutorId { get; set; }
        public IEnumerable<string> AutorNome { get; set; }
    }
}