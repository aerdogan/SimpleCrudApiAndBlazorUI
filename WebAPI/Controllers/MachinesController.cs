using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MachinesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public MachinesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("MachineAdd")]
        public IActionResult MachineAdd(MachineDTO machineDTO)
        {
            var addedEntity = _db.Entry(machineDTO.ToEntity());
            addedEntity.State = EntityState.Added;
            var changes = _db.SaveChanges();
            if (changes > 0) return Ok("Kayıt Eklendi");
            else return BadRequest("Hata");
        }

        [HttpPatch("MachineUpdate")]
        public IActionResult MachineUpdate(Machine machine_)
        {
            var dbMachine = _db.machines.Find(machine_.ID);
            if (dbMachine == null) return NotFound();
            PropertyUpdater.UpdateChangedProps(machine_, dbMachine, nameof(machine_.ID));
            var changes = _db.SaveChanges();
            if (changes > 0)
                return Ok("Kayıt Güncellendi");
            else
                return BadRequest("Hata");
        }

        public record DeleteDTO(int Id);

        [HttpDelete("MachineDelete")]
        public IActionResult MachineDelete([FromBody] DeleteDTO delete)
        {
            var tmp = _db.machines.Find(delete.Id);
            var ret = _db.machines.Remove(tmp);
            _db.SaveChanges();
            if (ret != null) return Ok("Kayıt Silindi");
            else return BadRequest("Hata");
        }

        [HttpGet("MachineList")]
        public IActionResult MachineList()
        {
            List<Machine> machines = _db.machines.ToList();
            return machines == null ? NotFound() : Ok(machines);
        }
    }
}
