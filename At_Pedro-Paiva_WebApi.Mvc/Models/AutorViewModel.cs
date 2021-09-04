using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace At_Pedro_Paiva_WebApi.Mvc.Models
{
    public class AutorViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime DtNasc { get; set; }
        public IEnumerable<int> LivroId { get; set; }
        public List<string> LivroNome { get; set; }
    }

    public class AutorCreateEditModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DtNasc { get; set; }
        public IEnumerable<int> LivroId { get; set; }
        public List<SelectListItem> LivroNome { get; set; }
    }
}