namespace ApiTarefasNet80.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        public string? Descricao { get; set; }

        public bool Feito { get; set; } = false;
        //implementar um algoritmo que marca quando foi feito (atualizado - tipo lista de tarefas) e adiciona a data de quando foi feito /taferas/id/feita

        public DateTime Data { get; set; } = DateTime.Now;
    }
}
