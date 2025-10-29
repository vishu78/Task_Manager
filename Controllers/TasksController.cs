using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Services;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        // GET /api/tasks
        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        // POST /api/tasks
        [HttpPost]
        public ActionResult<TaskItem> Create([FromBody] TaskItem newTask)
        {
            if (string.IsNullOrWhiteSpace(newTask.Description))
                return BadRequest("Description is required.");

            var created = _service.Add(newTask);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        // PUT /api/tasks/{id}
        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] TaskItem updated)
        {
            if (string.IsNullOrWhiteSpace(updated.Description))
                return BadRequest("Description is required.");

            var success = _service.Update(id, updated);
            if (!success) return NotFound();

            return NoContent();
        }

        // DELETE /api/tasks/{id}
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var success = _service.Delete(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
