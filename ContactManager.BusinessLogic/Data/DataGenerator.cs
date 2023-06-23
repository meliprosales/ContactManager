﻿using Bogus.DataSets;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ContactManager.BusinessLogic.Data
{
    public static class DataGenerator
    {
        public static readonly List<Contact> Contacts = new();

        public static readonly List<Address> Addresses = new();

        public const int NumberOfContacts = 5;

        public const int NumberOfAddress = 1;

        public static void InitData()
        {
            InitContactsData();
            InitAddressData(Contacts);
        }

        public static void InitContactsData()
        {
            var contactGenerator = GetContactGenerator(1);
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
            var generatedAddresses = addressGenerator.Generate(NumberOfAddress);
            return generatedAddresses;
        }

        private static Faker<Contact> GetContactGenerator(int id)
        {
            int nextId = id;
            return new Faker<Contact>()
                .RuleFor(e => e.Id, _ => nextId++)
                .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                .RuleFor(e => e.LastName, f => f.Name.LastName());
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