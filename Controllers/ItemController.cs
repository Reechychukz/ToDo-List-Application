using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo_List.Data;
using ToDo_List.Models;

namespace ToDo_List.Controllers
{
    public class ItemController : Controller
    {

        private readonly ToDoDBContext _db;
        public ItemController(ToDoDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Item> objList = _db.Item;
            return View(objList);
        }

        //GET-CREATE
        public IActionResult Create()
        {
            return View();
        }


        //POST-CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item obj)
        {
            if (ModelState.IsValid)
            {
                _db.Item.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
                
        }

        //GET-EDIT
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound("No Record Found");
            }
            var obj = _db.Item.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST-EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item obj)
        {
            if (ModelState.IsValid)
            {
                _db.Item.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);

        }

        //GET-DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Item.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST-DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTask(int? id)
        {
            var obj = _db.Item.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Item.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        //Search Action
        [HttpGet]
        public async Task<IActionResult> Index(string TaskSearch)
        {
            ViewData["GetTasks"] = TaskSearch;

            var taskQuery = from x in _db.Item select x;
            if (!string.IsNullOrEmpty(TaskSearch))
            {
                taskQuery = taskQuery.Where(x => x.Items.Contains(TaskSearch));
               
            }
            return View(await taskQuery.AsNoTracking().ToListAsync());
        }

        //GET-INFO
        public IActionResult Info(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Item.Find(id);
            if (obj.Info == null)
            {
                return NotFound("You did not record any detail for this task!");
            }
            return View(obj);
        }

    }
}
