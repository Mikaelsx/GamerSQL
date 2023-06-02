using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoGameSQL.Infra;
using ProjetoGameSQL.Models;

namespace ProjetoGameSQL.Controllers
{
    [Route("[controller]")]
    public class JogadorController : Controller
    {
        private readonly ILogger<JogadorController> _logger;

        public JogadorController(ILogger<JogadorController> logger)
        {
            _logger = logger;
        }

        Context c = new Context();

        [Route("Listar")] 
        public IActionResult Jindex()
        {
            ViewBag.Equipe = c.Equipe.ToList();
            ViewBag.Jogador = c.Jogador.ToList();

            return View();
        }
    }


}
