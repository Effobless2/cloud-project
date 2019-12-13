using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public ActionResult<List<TodoElement>> Get()
        {
            return TodoList;
        }

        [HttpGet("{id}")]
        public ActionResult<TodoElement> Details(int id)
        {
            try
            {
                return TodoList.First(todo => todo.Id == id);
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        [HttpPut]
        public ActionResult<int> Create(TodoElement todo)
        {
            todo.Id = TodoList.Count;
            TodoList.Add(todo);
           return todo.Id;
        }

        [HttpPatch("Check/{id}")]
        public ActionResult Check(int id)
        {
            try
            {
                TodoElement todo = TodoList.First(todo => todo.Id == id);
                todo.Checked = !todo.Checked;
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                TodoElement todo = TodoList.First(todo => todo.Id == id);
                TodoList.Remove(todo);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}")]
        public ActionResult Edit(int id,  [FromBody]TodoElement todo)
        {
            try
            {
                TodoElement ExistingTodo = TodoList.First(todo => todo.Id == id);
                ExistingTodo.Checked = todo.Checked;
                ExistingTodo.Content = todo.Content;
                ExistingTodo.Name = todo.Name;
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}