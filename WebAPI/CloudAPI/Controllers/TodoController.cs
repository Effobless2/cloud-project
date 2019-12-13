using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlService;

namespace CloudAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        public static List<TodoElement> TodoList { get; set; } = new List<TodoElement>()
        {
            new TodoElement()
            {
                Id = 0,
                Name = "Miam miam",
                Content = "Faire à manger",
                Checked = false
            },
            new TodoElement()
            {
                Id = 1,
                Name = "Duel",
                Content = "It's time to du-du-du-duel !",
                Checked = false
            },
            new TodoElement()
            {
                Id = 2,
                Name = "Sleep",
                Content = "Dodo Maison",
                Checked = true
            },
            new TodoElement()
            {
                Id = 3,
                Name = "Pas content",
                Content = "Grèver",
                Checked = true
            },
        };
        private readonly ISqlService sqlService;

        public TodoController(ISqlService _sqlService)
        {
            sqlService = _sqlService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoElement>>> Get()
        {
            try
            {
                List<TodoElement> result = await sqlService.Get();
                return result;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoElement>> Details(int id)
        {
            try
            {
                TodoElement result = await sqlService.Get(id);
                return result;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<ActionResult<int>> Create(TodoElement todo)
        {
            try
            {
                int result = await sqlService.Create(todo);
                return todo.Id;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPatch("Check/{id}")]
        public async Task<ActionResult> Check(int id)
        {
            try
            {
                await sqlService.Check(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await sqlService.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPatch]
        public async Task<ActionResult> Edit(TodoElement todo)
        {
            try
            {
                await sqlService.Edit(todo);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}