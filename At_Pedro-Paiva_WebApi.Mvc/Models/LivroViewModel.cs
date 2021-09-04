using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace At_Pedro_Paiva_WebApi.Mvc.Models
{
    public class LivroViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public DateTime Ano { get; set; }
        public List<int> AutorId { get; set; }
        public List<string> AutorNome { get; set; }
    }

    public class LivroCreateEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public DateTime Ano { get; set; }
        public int AutorId { get; set; }
        public List<SelectListItem> AutorNome { get; set; }
    }
}