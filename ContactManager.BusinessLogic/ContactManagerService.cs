using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.BusinessLogic
{
    public class ContactManagerService : IContactManagerService
    {
        ContactManagerContext _context;

        public ContactManagerService(ContactManagerContext context)
        {

            _context = context;
        }

        public IList<Contact> ContactsFrom(int contactManagerId)
        {
            var contacts = _context.Contacts
                .Where(c => c.ContactManager.Id == contactManagerId)
                .ToList();
            return contacts;
        }
    }
}