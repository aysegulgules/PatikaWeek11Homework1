using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PatikaWeek11Homework1.Models;

namespace PatikaWeek11Homework1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrazyMusiciansController : ControllerBase
    {
        List<Musician> _crazyMusicians = new List<Musician>()
        {
            new Musician (){ Id=1, Name="Ahmet Çalgı", Profession="Ünlü Çalgı Çalar", FunFeature="Her zaman yanlış nota çalar, ama çok eğlenceli."},
            new Musician (){ Id=2, Name="Zeynep Melodi", Profession="Popüler Melodi Yazarı", FunFeature="Şarkıları yanlış anlaşılır, ama çok popüler."},
            new Musician (){ Id=3, Name="Cemil Akor", Profession="Çılgın Akorist", FunFeature="Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli"}
        };

        [HttpGet]
        public IEnumerable<Musician> GetCrazyMusicianAll()
        {

            return _crazyMusicians;

        }

        [HttpGet("{Id:int:min(1)}")]
        public ActionResult GetCrazyMusician(int Id)
        {
           var crazyMusician= _crazyMusicians.Where(m=>m.Id==Id);
            if(crazyMusician is null)
            {
                return NotFound();
            }

            return Ok(crazyMusician);
        }

        [HttpGet("{name}")]
        public IActionResult GetCrazyMusicianByName(string name)
        {
            var crazyMusician=_crazyMusicians.Where(m=>m.Name.Equals(name,StringComparison.OrdinalIgnoreCase));

            if(!crazyMusician.Any())
            {
                return NotFound();
            }

            return Ok(crazyMusician);
        }

        [HttpPost]
        public IActionResult CreateCrazyMusician([FromBody] Musician musician)
        {
            var Id = _crazyMusicians.Max(x => x.Id)+1;
            musician.Id = Id;
            _crazyMusicians.Add(musician);

            return CreatedAtAction(nameof(GetCrazyMusicianAll), new { Id=musician.Id},musician);
        }

        [HttpPatch("update-prop/{Id:int:min(1)}/{funFeature}")]
        public  IActionResult PatchCrazyMusician(int id, string funFeature, [FromBody] JsonPatchDocument<Musician> patchDocument)
        {
            var crazyMusician = _crazyMusicians.FirstOrDefault(m => m.Id == id);

            if(crazyMusician is null)
            {
                return NotFound("Bulunamadı");
            }

            crazyMusician.FunFeature = funFeature;
            patchDocument.ApplyTo(crazyMusician);


            return NoContent();
        }
      
        [HttpPut("update")]
        public IActionResult UpdateCrazyMusician([FromBody] Musician musician,[FromQuery]int Id)
        {
            var crazyMusician=_crazyMusicians.Where(x=>x.Id==Id).FirstOrDefault();

            if (crazyMusician is null)
            {
                return NotFound("Güncellemek istenen kayıt bulunamadı.");
            }
            crazyMusician.Name= musician.Name;
            crazyMusician.Profession= musician.Profession;
            crazyMusician.FunFeature= musician.FunFeature;

            return NoContent();
        }


        [HttpDelete("{Id:int:min(1)}")]
        [HttpDelete("cansel/{name}")]
        public IActionResult DeleteCrazyMusician(int? Id,string? name) 
        {
            Musician crazyMusician;
            if(Id.HasValue)
            {
                crazyMusician=_crazyMusicians.Where(m => m.Id==Id).FirstOrDefault();
            }
            else
            {
                crazyMusician = _crazyMusicians.Where(m => m.Name.Equals(name,StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            }

            if(crazyMusician is null)
            {
                return NotFound("Muzisyen bulunamadı.");
            }

            _crazyMusicians.Remove(crazyMusician);

            return NoContent();
        }

    }
}
