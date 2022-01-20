using Microsoft.AspNetCore.Mvc;

namespace APIRestAlura.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceitasController : ControllerBase
    {
        //private static List<Receitas> receitas = new List<Receitas>
        //{
        //    new Receitas
        //    {
        //        Id = 1,
        //        Descricao = "Descri��o Inserida",
        //        Valor = 11.66M,
        //        DataReceita = DateTime.Now
        //    },
        //    new Receitas
        //    {
        //        Id = 2,
        //        Descricao = "Descri��o Inserida 2",
        //        Valor = 17.66M,
        //        DataReceita = DateTime.Now
        //    }
        //};

        private readonly DataContext _context;

        public ReceitasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Receitas>>> Get()
        {
            return Ok(await _context.Receitas.ToListAsync());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<List<Receitas>>> Get(int Id)
        {
            var receita = await _context.Receitas.FindAsync(Id);
            if (receita == null)
                return BadRequest("Receita n�o encontrada!");
            return Ok(receita);
        }

        [HttpPost]
        public async Task<ActionResult<List<Receitas>>> AddReceita(Receitas receita)
        {
            _context.Receitas.Add(receita);
            await _context.SaveChangesAsync();
            return Ok(await _context.Receitas.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Receitas>>> UpdateReceita(Receitas request)
        {
            var dbReceita = await _context.Receitas.FindAsync(request.Id);
            if (dbReceita == null)
                return BadRequest("Receita n�o encontrada!");
            
            dbReceita.Descricao = request.Descricao;
            dbReceita.Valor = request.Valor;
            dbReceita.DataReceita = request.DataReceita;

            await _context.SaveChangesAsync();
            return Ok(await _context.Receitas.ToListAsync());
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Receitas>>> Delete(int Id)
        {
            var dbReceita = await _context.Receitas.FindAsync(Id);
            if (dbReceita == null)
                return BadRequest("Receita n�o encontrada!");

            _context.Receitas.Remove(dbReceita);
            await _context.SaveChangesAsync();

            return Ok(await _context.Receitas.ToListAsync());
        }
    }
}