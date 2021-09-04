using At_Pedro_Paiva_WebApi.Api.Models;
using At_Pedro_Paiva_WebApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace At_Pedro_Paiva_WebApi.Api.Controllers
{
    public class AutorController : ApiController
    {
        private Model1Container db = new Model1Container();

        // GET: api/Autor
        [ResponseType(typeof(Autor))]
        public IEnumerable<AutorBindingModels> Get()
        {
            var autors = new List<AutorBindingModels>();
            var livrosIds = new List<int>();
            var livrosNomes = new List<string>();

            foreach (var autor in db.Autor)
            {
                if (autor.Livro.Count==0)
                {
                    autors.Add(new AutorBindingModels
                    {
                        Id = autor.Id,
                        Nome = autor.Nome,
                        Sobrenome = autor.Sobrenome,
                        Email = autor.Email,
                        DtNasc = autor.DtNasc,
                        LivroId = { }
                    });
                }
                else
                {
                    foreach (var livro in autor.Livro)
                    {
                        if (!livro.Equals(null))
                        {
                            livrosIds.Add(livro.Id);
                            livrosNomes.Add(livro.Titulo);
                        }
                    }
                    autors.Add(new AutorBindingModels
                    {
                        Id = autor.Id,
                        Nome = autor.Nome,
                        Sobrenome = autor.Sobrenome,
                        Email = autor.Email,
                        DtNasc = autor.DtNasc,
                        LivroId = livrosIds,
                        LivroNome = livrosNomes
                    });
                }
            }

            return autors;
        }

        // GET: api/Autor/5
        [ResponseType(typeof(AutorBindingModels))]
        public async Task<IHttpActionResult> Get(int id)
        {
            Autor dbautor = await db.Autor.FindAsync(id);

            if (dbautor == null)
            {
                return NotFound();
            }

            AutorBindingModels autor = new AutorBindingModels
            {
                Id = dbautor.Id,
                Nome = dbautor.Nome,
                Sobrenome = dbautor.Sobrenome,
                Email = dbautor.Email,
                DtNasc = dbautor.DtNasc
            };
            var livrosIds = new List<int>();
            var livrosNomes = new List<string>();

            foreach (var livro in dbautor.Livro)
            {
                //if(dbautor.Id)
                livrosIds.Add(livro.Id);
                livrosNomes.Add(livro.Titulo);
            }

            autor.LivroId = livrosIds;
            autor.LivroNome = livrosNomes;
            return Ok(autor);
        }

        // POST: api/Autor
        [ResponseType(typeof(Autor))]
        public async Task<IHttpActionResult> Post(AutorBindingModels model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Autor dbautor = new Autor
            {
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Email = model.Email,
                DtNasc = model.DtNasc
            };

            dbautor.Livro = new List<Livro>();

            if (model.LivroId != null)
            {
                foreach (var livrosId in model?.LivroId)
                {
                    var livros = new List<Livro>();
                    Livro dbLivro = await db.Livro.FindAsync(livrosId);
                    foreach (var l in livros)
                    {
                        if (livrosId.Equals(l.Id) || dbLivro == null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            dbautor.Livro.Add(dbLivro);
                            dbLivro.Autor.Add(dbautor);
                        }
                    }

                }
            }

            db.Autor.Add(dbautor);
            await db.SaveChangesAsync();

            model.Id = dbautor.Id;

            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }

        // PUT: api/Autor/5
        public async Task<IHttpActionResult> Put(int id, AutorBindingModels model)
        {
            Autor autor = await db.Autor.FindAsync(id);

            var livros = new List<Livro>();
            foreach (var l in db.Livro)
            {
                livros.Add(l);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != autor.Id)
            {
                return BadRequest();
            }
            else
            {
                autor.Nome = model.Nome;
                autor.Sobrenome = model.Sobrenome;
                autor.Email = model.Email;
                autor.DtNasc = model.DtNasc;

                if (model.LivroId != null && model.LivroNome != null)
                {
                    foreach (var livrosId in model?.LivroId)
                    {
                        foreach (var livro in livros)
                        {
                            if (livrosId.Equals(livro.Id))
                            {
                                autor.Livro.Add(livro);
                            }
                        }
                        Livro dbLivro = await db.Livro.FindAsync(livrosId);
                        foreach (var a in autor.Livro)
                        {
                            if (dbLivro.Id.Equals(a.Id) || dbLivro == null)
                            {
                            }
                            else
                            {
                                autor.Livro.Add(dbLivro);
                                dbLivro.Autor.Add(autor);
                            }
                        }
                    }
                }
                else
                {
                    autor.Livro.Clear();
                }
            }

            model.Id = id;
            await db.SaveChangesAsync();

            return Ok(model);
        }

        // DELETE: api/Autor/5
        [ResponseType(typeof(Autor))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Autor autor = await db.Autor.FindAsync(id);

            if (autor == null)
            {
                return NotFound();
            }

            foreach (var livro in db.Livro)
            {
                foreach (var autorlivro in livro.Autor)
                {
                    if (autorlivro.Id.Equals(id))
                    {
                        livro.Autor.Remove(autorlivro);
                        break;
                    }
                }
            }

            db.Autor.Remove(autor);
            await db.SaveChangesAsync();

            return Ok(autor);
        }
    }
}
