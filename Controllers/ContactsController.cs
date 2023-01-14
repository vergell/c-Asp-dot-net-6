using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ContactsController : Controller
    {

        private readonly ApplicationDbContext _db;
        public ContactsController(ApplicationDbContext db)
        {
            _db = db;
        }
        //INDEX
        public IActionResult Index(string searchString)
        {
            ViewData["currentFilter"] = searchString;
            var search = from b in _db.Contacts select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                search = search.Where(b => b.FirstName.Contains(searchString) || b.LastName.Contains(searchString) || b.Email.Contains(searchString));
            }
            return View(search);
        }
        //GET
        public IActionResult Create()
        {

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contacts obj)
        {
            var phone = obj.Phone;
            var email = obj.Email;
            var existingPhone = (from x in _db.Contacts where x.Phone == phone select x).ToList();
            var existingEmail = (from x in _db.Contacts where x.Email == email select x).ToList();

            if (ModelState.IsValid)
            {
                if (existingPhone.Count > 0)
                {
                    ModelState.AddModelError("Phone", "Mobile number " + phone + " already exist.");
                }
                if (existingEmail.Count > 0)
                {
                    ModelState.AddModelError("Email", "This Email " + email + " already exist.");

                }
                else
                {
                    _db.Contacts.Add(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(obj);
        }
        
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var contactsFromDb = _db.Contacts.Find(id);

            if (contactsFromDb == null)
            {
                return NotFound();
            }
            return View(contactsFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contacts obj)
        {

            if (ModelState.IsValid)
            {
  
                _db.Contacts.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
              
            }
            return View(obj);

        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var contactsFromDb = _db.Contacts.Find(id);

            if (contactsFromDb == null)
            {
                return NotFound();
            }
            return View(contactsFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Contacts.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            
            _db.Contacts.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
