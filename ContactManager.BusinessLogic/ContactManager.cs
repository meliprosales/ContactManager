using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.BusinessLogic
{
    public class ContactManager
    {
        List<Contact> contacs = new List<Contact>();

        string name;

        string lastName;

        public ContactManager() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public List<Contact> Contacts { get; set; }
    }
}
