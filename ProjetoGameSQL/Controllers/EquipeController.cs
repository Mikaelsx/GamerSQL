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
            //Forma de armazenamento (Mochila) que contem as Equipes 
            //Podemos usar essa Mochila na View de Equipe
            ViewBag.Equipe = c.Equipe.ToList();

            //Retorna a View de Equipe
            return View();
        }

        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe novaEquipe = new Equipe();

            novaEquipe.Nome = form["Nome"].ToString();
            novaEquipe.Imagem = form["Imagem"].ToString();

            c.Equipe.Add(novaEquipe);

            c.SaveChanges();

            ViewBag.Equipe = c.Equipe.ToList();

            return LocalRedirect("~/Equipe/Listar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}