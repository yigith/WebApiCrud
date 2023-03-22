using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCrud.Data;

namespace WebApiCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KisilerController : ControllerBase
    {
        private readonly UygulamaDbContext _context;

        public KisilerController(UygulamaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Kisi> GetKisiler()
        {
            return _context.Kisiler.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Kisi> GetKisi(int id)
        {
            Kisi? kisi = _context.Kisiler.Find(id);

            if (kisi == null)
                return NotFound();

            return kisi;
        }

        [HttpPost]
        public ActionResult<Kisi> PostKisi(Kisi kisi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kisi);
                _context.SaveChanges();
                return Created(Url.Action("GetKisi", new { id = kisi.Id})!, kisi);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteKisi(int id)
        {
            Kisi? kisi = _context.Kisiler.Find(id);

            if (kisi == null)
                return NotFound();

            _context.Remove(kisi);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult PutKisi(int id, Kisi kisi)
        {
            if (id != kisi.Id)
                return BadRequest();

            if (!_context.Kisiler.Any(x => x.Id == id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Update(kisi);
            _context.SaveChanges();
            return Ok();
        }
    }
}
