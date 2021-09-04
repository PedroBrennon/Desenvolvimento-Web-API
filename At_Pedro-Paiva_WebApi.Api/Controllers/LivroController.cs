using At_Pedro_Paiva_WebApi.Api.Models;
using At_Pedro_Paiva_WebApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace At_Pedro_Paiva_WebApi.Api.Controllers
{
    public class LivroController : ApiController
    {
        private Model1Container db = new Model1Container();

        // GET: api/Livro
        [ResponseType(typeof(Livro))]
        public IEnumerable<LivroBindingModels> Get()
        {
            var livros = new List<LivroBindingModels>();
            var autorIds = new List<int>();
            var autorNomes = new List<string>();

            foreach (var livro in db.Livro)
            {
                if (livro.Autor.Count == 0)
                {
                    livros.Add(new LivroBindingModels
                    {
                        Id = livro.Id,
                        Titulo = livro.Titulo,
                        ISBN = livro.ISBN,
                        Ano = livro.Ano,
                        AutorId = { },
                        AutorNome = { }
                    });
                }
                else
                {
                    foreach (var autor in livro.Autor)
                    {
                        if (!autor.Equals(null))
                        {
                            autorIds.Add(autor.Id);
                            autorNomes.Add(autor.Nome);
                        }
                    }
                    livros.Add(new LivroBindingModels
                    {
                        Id = livro.Id,
                        Titulo = livro.Titulo,
                        ISBN = livro.ISBN,
                        Ano = livro.Ano,
                        AutorId = autorIds,
                        AutorNome = autorNomes
                    });
                }
            }

            return livros;
        }

        // GET: api/Livro/5
        [ResponseType(typeof(LivroBindingModels))]
        public async Task<IHttpActionResult> Get(int id)
        {
            Livro dblivro = await db.Livro.FindAsync(id);

            if (dblivro == null)
            {
                return NotFound();
            }

            LivroBindingModels livro = new LivroBindingModels
            {
                Id = dblivro.Id,
                Titulo = dblivro.Titulo,
                ISBN = dblivro.ISBN,
                Ano = dblivro.Ano
            };
            var autorsId = new List<int>();
            var autorsNome = new List<string>();

            foreach (var autor in dblivro.Autor)
            {
                autorsId.Add(autor.Id);
                autorsNome.Add(autor.Nome);
            }

            livro.AutorId = autorsId;
            livro.AutorNome = autorsNome;

            return Ok(livro);
        }

        // POST: api/Livro
        [ResponseType(typeof(Livro))]
        public async Task<IHttpActionResult> Post(LivroCreateBindingModels model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Livro dblivro = new Livro
            {
                Titulo = model.Titulo,
                ISBN = model.ISBN,
                Ano = model.Ano
            };

            dblivro.Autor = new List<Autor>();

            if (!model.AutorId.Equals(0))
            {
                Autor dbautor = await db.Autor.FindAsync(model.AutorId);
                    if (dbautor == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        dblivro.Autor.Add(dbautor);
                        dbautor.Livro.Add(dblivro);
                    }
            }
            else
            {
            }

            db.Livro.Add(dblivro);
            await db.SaveChangesAsync();

            model.Id = dblivro.Id;

            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }

        // PUT: api/Livro/5
        public async Task<IHttpActionResult> Put(int id, LivroBindingModels model)
        {
            Livro livro = await db.Livro.FindAsync(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != livro.Id)
            {
                return BadRequest();
            }
            else
            {
                livro.Titulo = model.Titulo;
                livro.ISBN = model.ISBN;
                livro.Ano = model.Ano;

                if (model.AutorId != null)
                {
                    foreach (var autorsId in model?.AutorId)
                    {
                        Autor dbautor = await db.Autor.FindAsync(autorsId);
                        if (dbautor == null)
                        {
                            return NotFound();
                        }
                        livro.Autor.Add(dbautor);
                        dbautor.Livro.Add(livro);
                    }
                }
            }
            await db.SaveChangesAsync();

            return Ok(livro);
        }

        // DELETE: api/Livro/5
        [ResponseType(typeof(Livro))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Livro livro = await db.Livro.FindAsync(id);

            if (livro == null)
            {
                return NotFound();
            }

            foreach (var autor in db.Autor)
            {
                foreach (var livroAutor in autor.Livro)
                {
                    if (livroAutor.Id.Equals(id))
                    {
                        autor.Livro.Remove(livroAutor);
                        break;
                    }
                }
            }

            db.Livro.Remove(livro);
            await db.SaveChangesAsync();

            return Ok(livro);
        }
    }
}
