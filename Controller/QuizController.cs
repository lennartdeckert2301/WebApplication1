using LernmoduleApp.Data;
using LernmoduleApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LernmoduleApp.Controllers
{
    [ApiController]
    [Route("api/quiz")]
    public class QuizController : ControllerBase
    {
        private readonly AppDbContext _db;
        public QuizController(AppDbContext db) => _db = db;

        [HttpPost("result")]
        public async Task<IActionResult> SaveResult([FromBody] QuizResultDto dto)
        {
            if (dto == null || dto.Answers == null) return BadRequest("Invalid payload");

            var run = new QuizRun
            {
                ModuleIndex = dto.Module,
                TotalQuestions = dto.Answers.Count,
                CorrectCount = dto.Answers.Count(a => a.SelectedIndex == a.CorrectIndex),
                Answers = dto.Answers.Select(a => new QuizAnswer
                {
                    Question = a.Question ?? string.Empty,
                    SelectedIndex = a.SelectedIndex,
                    CorrectIndex = a.CorrectIndex
                }).ToList()
            };

            _db.QuizRuns.Add(run);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                run.Id,
                run.ModuleIndex,
                run.TotalQuestions,
                run.CorrectCount,
                run.CreatedAt
            });
        }

        [HttpGet("runs")]
        public async Task<IActionResult> GetRuns()
        {
            var runs = await _db.QuizRuns
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new {
                    r.Id, r.ModuleIndex, r.TotalQuestions, r.CorrectCount, r.CreatedAt
                })
                .ToListAsync();

            return Ok(runs);
        }
    }

    public class QuizResultDto
    {
        public int Module { get; set; }
        public List<AnswerDto> Answers { get; set; } = new();
    }

    public class AnswerDto
    {
        public string? Question { get; set; }
        public int? SelectedIndex { get; set; }
        public int CorrectIndex { get; set; }
    }
}
