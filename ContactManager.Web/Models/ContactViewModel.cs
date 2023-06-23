using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Web.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "The FirstName field is required.")]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        [Remote(action: "ValidateAddress", controller: "BingAddress")]
        public string FormattedAddress { get; set; }
    }
}
