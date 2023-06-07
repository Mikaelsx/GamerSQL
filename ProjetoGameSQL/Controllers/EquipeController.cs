using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjetoGameSQL.Infra;
using ProjetoGameSQL.Models;

namespace ProjetoGameSQL.Controllers
{
    [Route("[controller]")]
    public class EquipeController : Controller
    {
        private readonly ILogger<EquipeController> _logger;

        public EquipeController(ILogger<EquipeController> logger)
        {
            _logger = logger;
        }

        Context c = new Context();

        [Route("Listar")] //http:localhost/Equipe/Listar
        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            //Forma de armazenamento (Mochila) que contem as Equipes 
            //Podemos usar essa Mochila na View de Equipe
            ViewBag.Equipe = c.Equipe.ToList();

            //Retorna a View de Equipe
            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe novaEquipe = new Equipe();

            novaEquipe.Nome = form["Nome"].ToString();
            
             if (form.Files.Count > 0)
            {
                
                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                //gera o caminho completo até o caminho do arquivo(imagem - nome com extensão)
                var path = Path.Combine(folder, file.FileName);

                //using para que a instrução dentro dele seja encerrado assim que for executada
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.Imagem = file.FileName;
            }
            else
            {
                novaEquipe.Imagem = "Icon.png";
            }

            // novaEquipe.Imagem = form["Imagem"].ToString();

            c.Equipe.Add(novaEquipe);

            c.SaveChanges();

            ViewBag.Equipe = c.Equipe.ToList();

            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            Equipe e = c.Equipe.First( e => e.IdEquipe == id);

            c.Equipe.Remove(e);

            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");
        }


        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            Equipe e = c.Equipe.First(e => e.IdEquipe == id);

            ViewBag.Equipe = e;

            return View("Edit");
        }

        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form, Equipe e)
        {
            Equipe novaEquipe = new Equipe();

            novaEquipe.Nome = e.Nome;

            //Upload da imagem da Equipe nova
            if (form.Files.Count > 0)
            {
                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var path = Path.Combine(folder, file.FileName);

            using(var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            novaEquipe.Imagem = file.FileName;
            }

            else
            {
                novaEquipe.Imagem = "padrão.png";
            }

            Equipe equipe = c.Equipe.First(x => x.IdEquipe == e.IdEquipe);

            equipe.Nome = novaEquipe.Nome;
            equipe.Imagem = novaEquipe.Imagem;

            c.Equipe.Update(equipe);

            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");

        
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}