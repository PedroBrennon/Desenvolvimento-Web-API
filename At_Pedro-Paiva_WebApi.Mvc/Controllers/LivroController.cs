using At_Pedro_Paiva_WebApi.Mvc.Attributes;
using At_Pedro_Paiva_WebApi.Mvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace At_Pedro_Paiva_WebApi.Mvc.Controllers
{
    public class LivroController : Controller
    {
        private HttpClient _client;
        //private TokenHelper _helper;

        public LivroController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5195/");
            _client.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
        }

        // GET: Livro
        [Authentication]
        public async Task<ActionResult> Index()
        {
            var data = new List<LivroViewModel>();
            var response = await _client.GetAsync("api/Livro");

            if (response.IsSuccessStatusCode)
            {
                var livros = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<LivroViewModel>>(livros);
            }

            return View(data);
        }

        // GET: Livro/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var data = new LivroViewModel();
            var response = await _client.GetAsync($"api/Livro/{id}");

            if (response.IsSuccessStatusCode)
            {
                var livros = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<LivroViewModel>(livros);
            }

            return View(data);
        }

        // GET: Livro/Create
        public async Task<ActionResult> Create()
        {
            var data = new List<AutorViewModel>();
            var response = await _client.GetAsync($"api/Autor");
            if (response.IsSuccessStatusCode)
            {
                var autorLivros = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<AutorViewModel>>(autorLivros);
            }

            var livro = new LivroCreateEditViewModel();
            var list = new List<SelectListItem>();

            foreach (var autor in data)
            {
                var selectListItem = new SelectListItem
                {
                    Value = ((int)autor.Id).ToString(),
                    Text = autor.Nome + "" + autor.Sobrenome,
                    Selected = autor.Id == livro.AutorId
                };
                list.Add(selectListItem);
            }
            livro.AutorNome = list;

            return View(livro);
        }

        // POST: Livro/Create
        [HttpPost]
        public async Task<ActionResult> Create(LivroCreateEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _client.PostAsJsonAsync("api/Livro", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var autors = await response.Content.ReadAsStringAsync();
                    }
                    else { var autors = await response.Content.ReadAsStringAsync(); }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Ocorreu um erro. Tente novamente.";
                return View();
            }
        }

        // GET: Livro/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var data = new LivroCreateEditViewModel();
            var dataAutorLivros = new List<LivroCreateEditViewModel>();
            var response = await _client.GetAsync($"api/Livro/{id}");
            var responseAutorLivros = await _client.GetAsync($"api/Livro");

            if (response.IsSuccessStatusCode)
            {
                var livro = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<LivroCreateEditViewModel>(livro);
            }
            if (responseAutorLivros.IsSuccessStatusCode)
            {
                var livro = await response.Content.ReadAsStringAsync();
                dataAutorLivros = JsonConvert.DeserializeObject<List<LivroCreateEditViewModel>>(livro);
            }

            foreach (var autor in dataAutorLivros)
            {
                data = new LivroCreateEditViewModel
                {
                    AutorId = autor.AutorId,
                    AutorNome = autor.AutorNome
                };
            }
            data.AutorNome.Add(new SelectListItem() { Value = "", Text = "Nenhum" });

            return View(data);
        }

        // POST: Livro/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, LivroCreateEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _client.PutAsJsonAsync($"api/Livro/{id}", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var autors = await response.Content.ReadAsStringAsync();
                    }
                    else { }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Livro/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var data = new LivroViewModel();
            var response = await _client.GetAsync($"api/Livro/{id}");

            if (response.IsSuccessStatusCode)
            {
                var livros = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<LivroViewModel>(livros);
            }

            return View(data);
        }

        // POST: Livro/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, LivroViewModel livro)
        {
            try
            {
                var response = await _client.DeleteAsync($"api/Livro/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var autors = await response.Content.ReadAsStringAsync();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
