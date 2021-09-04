using At_Pedro_Paiva_WebApi.Mvc.Attributes;
using At_Pedro_Paiva_WebApi.Mvc.Helpers;
using At_Pedro_Paiva_WebApi.Mvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace At_Pedro_Paiva_WebApi.Mvc.Controllers
{
    public class AutorController : Controller
    {
        private HttpClient _client;
        //private TokenHelper _helper;

        public AutorController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5195/");
            _client.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
        }

        // GET: Autor
        [Authentication]
        public async Task<ActionResult> Index()
        {
            var data = new List<AutorViewModel>();
            var response = await _client.GetAsync($"api/Autor");

            if (response.IsSuccessStatusCode)
            {
                var autor = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<List<AutorViewModel>>(autor);
            }

            return View(data);
        }

        // GET: Autor/Details/5
        [Authentication]
        public async Task<ActionResult> Details(int id)
        {
            var data = new AutorViewModel();
            var response = await _client.GetAsync($"api/Autor/{id}");

            if (response.IsSuccessStatusCode)
            {
                var autors = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<AutorViewModel>(autors);
            }

            return View(data);
        }

        // GET: Autor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        [HttpPost]
        public async Task<ActionResult> Create(AutorCreateEditModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _client.PostAsJsonAsync("api/Autor", model);

                    if (response.IsSuccessStatusCode)
                    {
                        var autors = await response.Content.ReadAsStringAsync();
                    }
                    else{}
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Ocorreu um erro. Tente novamente.";
                return View();
            }
        }

        // GET: Autor/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var data = new AutorViewModel();
            var dataLivrosAutor = new List<AutorViewModel>();
            var response = await _client.GetAsync($"api/Autor/{id}");
            var responseLivrosAutor = await _client.GetAsync($"api/Autor");

            if (response.IsSuccessStatusCode)
            {
                var autor = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<AutorViewModel>(autor);
            }
            
            return View(data);
        }

        // POST: Autor/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, AutorViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _client.PutAsJsonAsync($"api/Autor/{id}", model);

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

        // GET: Autor/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var data = new AutorViewModel();
            var response = await _client.GetAsync($"api/Autor/{id}");

            if (response.IsSuccessStatusCode)
            {
                var autors = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<AutorViewModel>(autors);
            }

            return View(data);
        }

        // POST: Autor/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, AutorViewModel model)
        {
            try
            {
                var response = await _client.DeleteAsync($"api/Autor/{id}");

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
