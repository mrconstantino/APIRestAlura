using Microsoft.AspNetCore.Mvc;

namespace APIRestAlura.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DespesasController : ControllerBase
    {
        //[HttpGet(Name = "GetDespesas")]
        //public IEnumerable<Despesas> Get()
        //{
        //    return Enumerable.Range(1, 1).Select(index => new Despesas
        //    {
        //        Id = 1,
        //        Descricao = "Descricao Teste",
        //        Valor = 10.55M,
        //        DataReceita = DateTime.Now
        //    })
        //    .ToArray();
        //}

        private readonly DataContext _context;

        public DespesasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Despesas>>> Get()
        {
            return Ok(await _context.Despesas.ToListAsync());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<List<Despesas>>> Get(int Id)
        {
            var despesa = await _context.Despesas.FindAsync(Id);
            if (despesa == null)
                return BadRequest("Despesa não encontrada!");
            return Ok(despesa);
        }

        [HttpPost]
        public async Task<ActionResult<List<Despesas>>> AddDespesa(Despesas despesa)
        {
            _context.Despesas.Add(despesa);
            await _context.SaveChangesAsync();
            return Ok(await _context.Despesas.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Despesas>>> UpdateDespesa(Despesas request)
        {
            var dbDespesa = await _context.Despesas.FindAsync(request.Id);
            if (dbDespesa == null)
                return BadRequest("Despesa não encontrada!");

            dbDespesa.Descricao = request.Descricao;
            dbDespesa.Valor = request.Valor;
            dbDespesa.DataDespesa = request.DataDespesa;

            await _context.SaveChangesAsync();
            return Ok(await _context.Despesas.ToListAsync());
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Despesas>>> Delete(int Id)
        {
            var dbDespesa = await _context.Despesas.FindAsync(Id);
            if (dbDespesa == null)
                return BadRequest("Despesa não encontrada!");

            _context.Despesas.Remove(dbDespesa);
            await _context.SaveChangesAsync();

            return Ok(await _context.Despesas.ToListAsync());
        }
    }
}
