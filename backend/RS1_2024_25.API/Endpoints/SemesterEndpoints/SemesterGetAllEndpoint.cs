using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models.SharedTables;
using RS1_2024_25.API.Data.Models.TenantSpecificTables.Modul1_Auth;
using RS1_2024_25.API.Data.Models.TenantSpecificTables.Modul2_Basic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Endpoints.SemesterEndpoints
{
    [Route("semester")]
    public class SemesterGetAllEndpoint(ApplicationDbContext db) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSemstersByStudent(int id, CancellationToken cancellationToken = default)
        {
            var semesters = await db.SemestersAll.Include(x => x.Student).Include(x => x.Profesor).Include(x => x.AkademskaGodina).Where(x => x.StudentId == id)
                 .Select(x => new SemesterReadResponse
                 {
                     Id = x.Id,
                     StudentId = x.StudentId,
                     Student=x.Student,
                     Profesor=x.Profesor,
                     ProfesorId = x.ProfesorId,
                     DatumUpisa = x.DatumUpisa,
                     GodinaStudija = x.GodinaStudija,
                     AkademskaGodina=x.AkademskaGodina,
                     AkademskaGodinaId = x.AkademskaGodinaId,
                     CijenaSkolarine = x.CijenaSkolarine,
                     Obnova = x.Obnova,
                 }).ToListAsync(cancellationToken);

            return Ok(semesters);
        }

    }

    public class SemesterReadResponse
    {
        public required int Id { get; set; }
        public required int StudentId { get; set; }
        public Student? Student { get; set; }
        public required int ProfesorId { get; set; }
        public MyAppUser? Profesor { get; set; }
        public required DateTime DatumUpisa { get; set; }
        public required int GodinaStudija { get; set; }
        public required int AkademskaGodinaId { get; set; }
        public AcademicYear? AkademskaGodina { get; set; }
        public required float CijenaSkolarine { get; set; }
        public required bool Obnova { get; set; }
    }
}
