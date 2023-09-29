using Microsoft.AspNetCore.Mvc;
using Protocolo_2_teste.Models;
using Protocolo_2_teste.Data;
using Protocolo_2_teste.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Protocolo_2_teste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OperadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddOperador([FromBody] Operador operador)
        {
            try
            {
                _context.Operadores.Add(operador);
                _context.SaveChanges();

                return Ok(operador);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public IActionResult GetOperadores()
        {
            try
            {
                var operadores = _context.Operadores.ToList();
                return Ok(operadores);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetOperador(int id)
        {
            try
            {
                var operador = _context.Operadores.Find(id);
                if (operador == null)
                {
                    return NotFound();
                }
                return Ok(operador);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateOperador(int id, [FromBody] Operador operador)
        {
            try
            {
                if (id != operador.Id)
                {
                    return BadRequest();
                }

                _context.Entry(operador).State = EntityState.Modified;
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteOperador(int id)
        {
            try
            {
                var operador = _context.Operadores.Find(id);
                if (operador == null)
                {
                    return NotFound();
                }

                _context.Operadores.Remove(operador);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public IActionResult Autenticar(string nome, string senha)
        {
            Servidor servidor = new Servidor(); // Instancia um objeto Servidor
            if (servidor.AutenticarOperador(nome, senha))
            {
                // Autenticação bem-sucedida
                return RedirectToAction("Index", "Home"); // Redireciona para a página inicial, ajuste conforme necessário
            }
            else
            {
                // Autenticação falhou
                ModelState.AddModelError(string.Empty, "Nome de usuário ou senha incorretos."); // Adiciona uma mensagem de erro ao ModelState
                return View(""); // Retorna a mesma página de autenticação com a mensagem de erro, ajuste conforme necessário
            }
        }




    }
}
