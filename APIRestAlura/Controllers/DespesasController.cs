using Microsoft.AspNetCore.Mvc;

namespace APIRestAlura.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DespesasController : ControllerBase
    {
        [HttpGet(Name = "GetDespesas")]
        public IEnumerable<Despesas> Get()
        {
            return Enumerable.Range(1, 1).Select(index => new Despesas
            {
                Id = 1,
                Descricao = "Descricao Teste",
                Valor = 10.55M,
                DataReceita = DateTime.Now
            })
            .ToArray();
        }
    }
}
