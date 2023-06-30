using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.BusinessLogic
{
    public interface IContactManagerService
    {
        IList<Contact> ContactsFrom(int contactManagerId);
    }
}
