using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using weekly.Models;
using weekly.Services;

namespace weekly.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoService _toDos;

        public ToDoController(ToDoService toDoService)
        {
            _toDos = toDoService;
        }

        [HttpGet]
        public ActionResult<List<ToDo>> Get() =>
            _toDos.Get();

        [HttpGet("{id:length(24)}", Name = "GetToDo")]
        public ActionResult<ToDo> Get(string id)
        {
            var todo = _toDos.Get(id);
            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        [HttpPost]
        public ActionResult<ToDo> Create(ToDo todo)
        {
            DateService dateService = new DateService();
            dateService.GetCurrentWeek();
            _toDos.Create(todo);
            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, ToDo todoIn)
        {
            var todo = _toDos.Get(id);
            if (todo == null)
            {
                return NotFound();
            }
            _toDos.Update(id,todoIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var todo = _toDos.Get(id);
            if (todo == null)
            {
                return NotFound();
            }
            _toDos.Remove(id);
            return NoContent();
        }
        
        
    }
}