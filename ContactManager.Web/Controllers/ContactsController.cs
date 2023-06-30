using AutoMapper;
using ContactManager.BusinessLogic;
using ContactManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Web.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactManagerContext _context;
        private readonly IContactManagerService _contactManagerService;
        private readonly IMapper _mapper;

        public ContactsController(ContactManagerContext context, IMapper mapper, IContactManagerService contactManagerService)
        {
            _context = context;
            _mapper = mapper;
            _contactManagerService = contactManagerService;
        }

        // GET: ContactsController
        public IActionResult Index()
        {
            return View();
        }

        //GET: ContactsController/Search
        public IActionResult Search(string searchTerm)
        {
            var items = new List<Contact>();
            if (searchTerm == null)
            {
                items = _context.Contacts.Include(item => item.Address).ToList();
            }
            else
            {
                items = _context.Contacts
                   .Include(item => item.Address)
                   .Where(item => item.FirstName.Contains(searchTerm)
                   || item.LastName.Contains(searchTerm)
                   || item.Address.Street.Contains(searchTerm)
                   || item.Address.City.Contains(searchTerm)
                   || item.Address.State.Contains(searchTerm)
                   || item.Address.PostalCode.Contains(searchTerm))
                   .ToList();
            }
            return PartialView("_SearchResults", items);
        }

        // GET: ContactsController/Details/5
        public IActionResult Details(int? id)
        {
            var item = _context.Contacts
                .Include(s => s.Address)
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return PartialView("_Details", item);
        }

        // GET: ContactsController/Create
        public IActionResult Create()
        {
            return View(new ContactViewModel());
        }

        // POST: ContactsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var contact = new Contact { FirstName = model.FirstName, LastName = model.LastName };
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            var address = new BusinessLogic.Address { Street = model.Street, City = model.City, PostalCode = model.PostalCode, State = model.State, ContactId = contact.Id };
            _context.Addresses.Add(address);
            _context.SaveChanges();
            return RedirectToAction("Index", "Contacts"); // Redirect to the desired action and controller
        }

        // GET: ContactsController/Edit/5
        public ActionResult Edit(int id)
        {
            var contact = _context.Contacts
                .Include(item => item.Address)
                .FirstOrDefault(item => item.Id == id);
            var model = _mapper.Map<ContactViewModel>(contact);
            return View(model);
        }

        // POST: ContactsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var contact = _mapper.Map<Contact>(model);
            var address = contact.Address;

            contact.Id = id;
            _context.Entry(contact).State = EntityState.Modified;
            _context.SaveChanges();
            var dbContact = _context.Contacts
               .Include(item => item.Address)
               .AsNoTracking()
               .FirstOrDefault(item => item.Id == id);
            address.Id = dbContact.Address.Id;
            address.ContactId = id;
            _context.Entry(address).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index", "Contacts"); // Redirect to the desired action and controller
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var item = _context.Contacts.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Contacts.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
