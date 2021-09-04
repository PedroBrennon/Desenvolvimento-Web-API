using At_Pedro_Paiva_WebApi.Mvc.Helpers;
using At_Pedro_Paiva_WebApi.Mvc.Models;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
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
    public class ContaController : Controller
    {
        private HttpClient _client;
        private TokenHelper _helper;

        public ContaController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5195/");
            _client.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);

            _helper = new TokenHelper();
        }

        // GET: Conta
        public ActionResult Index()
        {
            return View();
        }
        //GET Conta/Register
        public ActionResult Cadastro()
        {
            return View();
        }
        //POST Conta/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastro(CadastroViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _client.PostAsJsonAsync("api/Account/Register", registerViewModel);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    return HttpNotFound();
                }
;
            }
            return View(registerViewModel);
        }
        //GET Conta/Login
        public ActionResult Login()
        {
            return View();
        }
        //POST Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var data = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "username", loginViewModel.Email },
                    { "password", loginViewModel.Password }
                };

                using (var requestContent = new FormUrlEncodedContent(data))
                {
                    var response = await _client.PostAsync("/Token", requestContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var tokenData = JObject.Parse(responseContent);

                        _helper.AccessToken = tokenData["access_token"];

                        ViewBag.Message = "Login efetuado com sucesso!";
                        return RedirectToAction("Index");
                    }
                    else
                    {

                    }
                }
;
            }
            return View(loginViewModel);
        }

        public async Task<ActionResult> Logout()
        {
            var data = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "username", "email" },
                    { "password", "password" }
                };
            var content = new FormUrlEncodedContent(data);
            var response = await _client.PostAsync("api/Account/Logout", content);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Message = $"Deslogado com sucesso";
                return RedirectToAction("Register");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _client != null)
            {
                _client.Dispose();
                _client = null;
            }
            base.Dispose(disposing);
        }

    }
}
