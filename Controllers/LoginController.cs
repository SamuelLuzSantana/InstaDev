using System.Collections.Generic;
using Instadev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Instadev.Controllers
{
    public class LoginController : Controller
    {
        [TempData]
        public string Mensagem { get; set; }

        Usuario usuarioModel = new Usuario();
        

        public IActionResult Index()
        {
            // var userId = HttpContext.Session.GetString("_UserId");
            // ViewBag.UserLogado = usuarioModel.ObterUsuarioDaSessao(int.Parse(userId));

            return View();
        }

        [Route("Logar")]
        public IActionResult Logar(IFormCollection form)
        {
            List<string> csv = usuarioModel.ReadAllLinesCSV(usuarioModel._PATH);

            var logado = csv.Find(
                x => 
                x.Split(";")[5] == form["Email"] && 
                x.Split(";")[6] == form["Senha"]
            );

            if(logado != null)
            {
                //Criamos uma sessão com os dados do usuário
                HttpContext.Session.SetString("_UserId", logado.Split(";")[0]);
                return LocalRedirect("~/Feed");
            }

            Mensagem = "Tente novamente.";


            return LocalRedirect("~/");
        }

        [Route("Deslogar")]
        public IActionResult Deslogar()
        {
            HttpContext.Session.Remove("_UserId");            
            return LocalRedirect("~/");
        }
    }
}