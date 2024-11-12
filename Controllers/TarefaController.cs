using ApiTarefasNet80.DTOs;
using ApiTarefasNet80.Models;
using Microsoft.AspNetCore.Mvc; // O namespace Microsoft.AspNetCore.Mvc fornece atributos que podem ser usados para configurar o comportamento de controladores de API Web e dos métodos de ação

// Veja uma lista que inclui os atributos disponíveis no namespace Microsoft.AspNetCore.Mvc. https://learn.microsoft.com/pt-br/dotnet/api/microsoft.aspnetcore.mvc?view=aspnetcore-8.0

namespace ApiTarefasNet80.Controllers
{
    [Route("tarefas")] //define a rota base da API
    [ApiController] //atributo do namespace .Mvc
    public class TarefaController : Controller // Cria a classe pública e herda da classe Controller (do namespace MCV), tornando-a um controlador MVC e habilitando métodos de respostas a requisições HTTP
    {
        [HttpGet] // Identifica uma ação que dá suporte ao verbo de ação HTTP GET.
        public IActionResult Get() //IActionResult - Interface? Define um contrato que representa o resultado de um método de ação.
        // Método público que retorna um IActionResult, usado para responder com diferentes tipos de resposta HTTP (por exemplo, Ok, NotFound, Problem).

        {
            try
            {
                List<Categoria> listaTarefas = new TarefaDAO().List(); // cria uma lista do tipo Tarefa, de nome listaTarefas, atribuí um novo objeto com base na classe TarefaDAO, acessando seu método List()

                return Ok(listaTarefas);
            }
            catch (Exception)
            {
                return Problem($"Ocorreram erros ao processar a solicitação");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var tarefa = new TarefaDAO().GetById(id);

                if (tarefa == null)
                {
                    return NotFound();
                }

                return Ok(tarefa);
            }
            catch (Exception)
            {
                return Problem("Ocorreram erros ao processar a solicitação");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] TarefaDTO item)
        {
            var tarefa = new Categoria();

            tarefa.Descricao = item.Descricao;
            tarefa.Feito = item.Feito;
            tarefa.Data = DateTime.Now;

            try
            {
                var dao = new TarefaDAO();
                tarefa.Id = dao.Insert(tarefa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Created("", tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TarefaDTO item)
        {
            try
            {
                var tarefa = new TarefaDAO().GetById(id);

                if (tarefa == null)
                {
                    return NotFound();
                }

                tarefa.Descricao = item.Descricao;
                tarefa.Feito = item.Feito;

                new TarefaDAO().Update(tarefa);

                return Ok(tarefa);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var tarefa = new TarefaDAO().GetById(id);

                if (tarefa == null)
                {
                    return NotFound();
                }

                new TarefaDAO().Delete(tarefa.Id);

                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
