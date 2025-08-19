using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Dtos;
using TaskApi.Models;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public TasksController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // GET: api/Tasks?search=&isDone=&page=1&pageSize=10&sort=createdAt_desc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetAll(
            string? search, bool? isDone, int page = 1, int pageSize = 10, string sort = "createdAt_desc")
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0 || pageSize > 100) pageSize = 10;

            var query = _db.Tasks.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(t => t.Title.Contains(search) || (t.Description ?? "").Contains(search));

            if (isDone.HasValue)
                query = query.Where(t => t.IsDone == isDone.Value);

            query = sort.ToLower() switch
            {
                "title_asc" => query.OrderBy(t => t.Title),
                "title_desc" => query.OrderByDescending(t => t.Title),
                "createdat_asc" => query.OrderBy(t => t.CreatedAt),
                _ => query.OrderByDescending(t => t.CreatedAt) // createdAt_desc default
            };

            var total = await query.CountAsync();
            Response.Headers["X-Total-Count"] = total.ToString();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TaskReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(items);
        }

        // GET: api/Tasks/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskReadDto>> GetById(int id)
        {
            var task = await _db.Tasks.AsNoTracking()
                .ProjectTo<TaskReadDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.Id == id);

            return task is null ? NotFound() : Ok(task);
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<TaskReadDto>> Create([FromBody] TaskCreateDto input)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = _mapper.Map<TaskItem>(input);
            _db.Tasks.Add(entity);
            await _db.SaveChangesAsync();

            var dto = _mapper.Map<TaskReadDto>(entity);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskUpdateDto input)
        {
            if (id != input.Id) return BadRequest("Id mismatch.");

            var entity = await _db.Tasks.FindAsync(id);
            if (entity is null) return NotFound();

            _mapper.Map(input, entity);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _db.Tasks.FindAsync(id);
            if (entity is null) return NotFound();

            _db.Tasks.Remove(entity);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}


