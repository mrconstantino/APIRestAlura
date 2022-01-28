using Microsoft.AspNetCore.Mvc;

namespace APIRestAlura.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DespesasController : ControllerBase
    {
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

        [HttpGet("GetById/{Id}")]
        public async Task<ActionResult<List<Despesas>>> GetById(int Id)
        {
            var despesa = await _context.Despesas.FindAsync(Id);
            if (despesa == null)
                return BadRequest("Despesa não encontrada!");
            return Ok(despesa);
        }

        [HttpGet("GetByDescription/{Descricao}")]
        public async Task<ActionResult<List<Despesas>>> GetByDescription(string? Descricao)
        {
            if (string.IsNullOrEmpty(Descricao))
            {
                return Ok(await _context.Despesas.ToListAsync());
            }
            else
            {
                var despesa = _context.Despesas.AsQueryable();

                despesa = despesa.Where(x => x.Descricao.Contains(Descricao));
                if (despesa.Count() == uint.MinValue)
                    return BadRequest("Despesa não encontrada!");
                return Ok(await despesa.ToListAsync());
            }            
        }

        [HttpGet("GetByReference/{Ano}/{Mes}")]
        public async Task<ActionResult<List<Despesas>>> GetByReference(int Ano, int Mes)
        {
            var despesa = _context.Despesas.AsQueryable();

            despesa = despesa.Where(x => x.DataDespesa.Year == Ano && x.DataDespesa.Month == Mes);
            if (despesa.Count() == uint.MinValue)
                return BadRequest("Despesa não encontrada!");
            return Ok(await despesa.ToListAsync());
            
        }

        [HttpPost]
        public async Task<ActionResult<List<Despesas>>> AddDespesa(Despesas despesa)
        {
            if(_context.Despesas.Count() > uint.MinValue)
            {
                var dbDespesa = _context.Despesas.Where(x => despesa.Descricao == x.Descricao &&
                despesa.DataDespesa.Year == x.DataDespesa.Year &&
                despesa.DataDespesa.Month == x.DataDespesa.Month).SingleOrDefault();

                if (dbDespesa != null)
                    return BadRequest("Despesa já cadastrada!");
            }

            if(despesa.Categoria == null || despesa.Categoria == uint.MinValue)
            {
                despesa.Categoria = Enumerators.Despesas.EnumCategoria.Outras;
            }

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

            if (_context.Despesas.Count() > uint.MinValue)
            {
                var dbDespesaUpd = _context.Despesas.Where(x => request.Descricao == x.Descricao &&
                request.DataDespesa.Year == x.DataDespesa.Year &&
                request.DataDespesa.Month == x.DataDespesa.Month).SingleOrDefault();

                if (dbDespesaUpd != null)
                    return BadRequest("Despesa já cadastrada!");
            }

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
