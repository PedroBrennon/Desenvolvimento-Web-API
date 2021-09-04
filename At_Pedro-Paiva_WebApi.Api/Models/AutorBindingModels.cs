using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace At_Pedro_Paiva_WebApi.Api.Models
{
    public class AutorBindingModels
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DtNasc { get; set; }
        public IEnumerable<int> LivroId { get; set; }
        public IEnumerable<string> LivroNome { get; set; }
    }
}