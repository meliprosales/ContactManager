using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.BusinessLogic
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }

        public override string ToString()
        {
            return Street + ", " + City + ", " + State + " " + PostalCode + ", United States";
        }
    }
}
