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
        //        Descricao = "Descrição Inserida",
        //        Valor = 11.66M,
        //        DataReceita = DateTime.Now
        //    },
        //    new Receitas
        //    {
        //        Id = 2,
        //        Descricao = "Descrição Inserida 2",
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
                return BadRequest("Receita não encontrada!");
            return Ok(receita);
        }

        [HttpPost]
        public async Task<ActionResult<List<Receitas>>> AddReceita(Receitas receita)
        {
            if (_context.Receitas.Count() > uint.MinValue)
            {
                var dbReceita = _context.Receitas.Where(x => receita.Descricao == x.Descricao &&
                receita.DataReceita.Year == x.DataReceita.Year &&
                receita.DataReceita.Month == x.DataReceita.Month).SingleOrDefault();

                if (dbReceita != null)
                    return BadRequest("Receita já cadastrada!");
            }

            _context.Receitas.Add(receita);
            await _context.SaveChangesAsync();
            return Ok(await _context.Receitas.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Receitas>>> UpdateReceita(Receitas request)
        {
            var dbReceita = await _context.Receitas.FindAsync(request.Id);
            if (dbReceita == null)
                return BadRequest("Receita não encontrada!");

            if (_context.Receitas.Count() > uint.MinValue)
            {
                var dbReceitaUpd = _context.Receitas.Where(x => request.Descricao == x.Descricao &&
                request.DataReceita.Year == x.DataReceita.Year &&
                request.DataReceita.Month == x.DataReceita.Month).SingleOrDefault();

                if (dbReceitaUpd != null)
                    return BadRequest("Receita já cadastrada!");
            }

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
                return BadRequest("Receita não encontrada!");

            _context.Receitas.Remove(dbReceita);
            await _context.SaveChangesAsync();

            return Ok(await _context.Receitas.ToListAsync());
        }
    }
}