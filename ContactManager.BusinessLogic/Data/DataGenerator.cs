using Bogus.DataSets;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.Extensions.Configuration;

namespace ContactManager.BusinessLogic.Data
{
    public static class DataGenerator
    {
        public static readonly List<Contact> Contacts = new();

        public static readonly List<Address> Addresses = new();

        public static readonly List<ContactManager> ContactManagers = new();

        public static int NumberOfContacts = 5;

        public static void InitData()
        {
            InitContactsData();
            InitAddressData(Contacts);
        }

        public static void InitContactsData()
        {
            var contactManager = new ContactManager { Name = "Test", LastName = "Test", Id=1 };
            ContactManagers.Add(contactManager);

            var contactGenerator = GetContactGenerator(1, contactManager);
            var generatedContacts = contactGenerator.Generate(NumberOfContacts);
            Contacts.AddRange(generatedContacts);
        }

        public static void InitAddressData(List<Contact> contacts)
        {
            contacts.ForEach(e => Addresses.AddRange(GesAddressData(e.Id, e.Id)));
        }

        private static List<Address> GesAddressData(int contactId, int id)
        {
            var addressGenerator = GetAddressGenerator(contactId, id);
            var generatedAddresses = addressGenerator.Generate(1);
            return generatedAddresses;
        }

        private static Faker<Contact> GetContactGenerator(int id, ContactManager contactManager)
        {
            int nextId = id;
            return new Faker<Contact>()
                .RuleFor(e => e.Id, _ => nextId++)
                .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                .RuleFor(e => e.LastName, f => f.Name.LastName())
                .RuleFor(e => e.ContactManagerId, _ => contactManager.Id);
        }

        private static Faker<Address> GetAddressGenerator(int contactId, int id)
        {
            int nextId = id;
            var faker = new Faker("en_US");
            var address = faker.Address;
            return new Faker<Address>()
                .RuleFor(e => e.Id, _ => nextId++)
                .RuleFor(v => v.ContactId, _ => contactId)
                .RuleFor(v => v.Street, _ => address.StreetAddress())
                .RuleFor(v => v.City, _ => address.City())
                .RuleFor(v => v.State, _ => address.State())
                .RuleFor(v => v.PostalCode, _ => address.ZipCode());
        }
    }
}