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
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Equipe = c.Equipe.ToList();
            ViewBag.Jogador = c.Jogador.ToList();

            return View();
        } 
        
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();

            novoJogador.Nome = form["Nome"].ToString();
            novoJogador.Email = form["Email"].ToString();
            novoJogador.Senha = form["Senha"].ToString();
            novoJogador.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            c.Jogador.Add(novoJogador);
            c.SaveChanges();
            return LocalRedirect("~/Jogador/Listar");
        }

        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            Jogador a = c.Jogador.First(a => a.IdJogador == id);

            c.Jogador.Remove(a);

            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            Jogador a = c.Jogador.First(a => a.IdJogador == id);

            ViewBag.Jogador = a;

            ViewBag.Equipe = c.Equipe.ToList();

            return View("JEdit");
        }

        
        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador(); 

            novoJogador.IdJogador = int.Parse(form["IdJogador"].ToString());//6
            novoJogador.Nome = form["Nome"].ToString();
            novoJogador.Email = form["Email"].ToString();
            novoJogador.Senha = form["Senha"].ToString();
            novoJogador.IdEquipe = int.Parse(form["IdEquipe"].ToString());//3

            // Console.WriteLine($"NOVO ID {novoJogador.IdJogador}");
            // Console.WriteLine($"NOVO NOME {novoJogador.Nome}");
            // Console.WriteLine($"NOVO EMAIL {novoJogador.Email}");
            // Console.WriteLine($"NOVO EMAIL {novoJogador.Email}");
        

            Jogador jogadorbuscado = c.Jogador.First(a => a.IdJogador == novoJogador.IdJogador);

            jogadorbuscado.Nome = novoJogador.Nome;
            jogadorbuscado.Email = novoJogador.Email;
            jogadorbuscado.Senha = novoJogador.Senha;
            jogadorbuscado.IdEquipe = novoJogador.IdEquipe;

            c.Jogador.Update(jogadorbuscado);

            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }
    }
}
