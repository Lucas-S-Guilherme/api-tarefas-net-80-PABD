using Microsoft.AspNetCore.Mvc;
using ApiTarefasNet80.Models;
using ApiTarefasNet80.DTOs;

namespace ApiTarefasNet80.ControllersCategoria {

    [Route("categorias")]
    [ApiController]

    public class CategoriaController : Controller {
        [HttpGet]

        public IActionResult Get()

        {
            try List<Categoria> categorias = new CategoriaDAO().List();
        }
    }
}