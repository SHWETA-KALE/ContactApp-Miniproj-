using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactApp_Miniproj_3_.Exceptions;
using ContactApp_Miniproj_3_.Models;
using ContactApp_Miniproj_3_.Services;

namespace ContactApp_Miniproj_3_.Repository
{
    internal class ContactRepo
    {
        // staff performs crud on contacts and contactDetails

        public static List<Contact> contacts = new List<Contact>();

        public ContactRepo()
        {
            contacts = ContactStorageSerialization.DeserializeContactList();

        }

        public bool CheckIsActive(int contactId)
        {
            foreach (var item in contacts)
            {
                if (item.ContactId == contactId && item.IsActive)
                {
                    return true;
                }
            }
            return false;
        }

        public Contact GetById(int contactId)
        {
            return contacts.FirstOrDefault(x => x.ContactId == contactId);
        }

        public bool ContactExists(int contactId)
        {
            return contacts.Any(c => c.ContactId == contactId);
        }


        public void AddContact(Contact contact)
        {
            contacts.Add(contact);
            SaveChanges();
        }

        public void UpdateContact(Contact updateContact)
        {
            var existingContact = updateContact;
            if (existingContact != null)
            {
                existingContact.FName = updateContact.FName;
                existingContact.LName = updateContact.LName;
                existingContact.IsActive = updateContact.IsActive;
            }
            SaveChanges();
        }

        public void DeleteContact(Contact deleteContact)
        {
            var contactExist = deleteContact;
            SaveChanges();
        }



        public void DisplayAllContacts()
        {
            var activeContacts = contacts.Where((contact) => contact.IsActive).ToList();
            activeContacts.ForEach(contact => Console.WriteLine(contact));
        }

        public void AddContactDetailsToContact(int contactId, Contact_Details contact_Details)
        {
            var contact = GetById(contactId);
            if (contact != null && contact.IsActive)
            {
                contact.Details.Add(contact_Details);
                SaveChanges();
            }
            else
            {
                throw new ContactDoesNotExistException("Cannot add details to an inactive or non-existent contact.");
            }
        }

        public void RemoveContactDetailsFromContact(int contactId, int contactDetailsId)
        {
            var contact = GetById(contactId);
            if (contact != null && contact.IsActive)
            {
                var contactDetail = contact.Details.FirstOrDefault(cd => cd.ContactDetailsId == contactDetailsId);
                if (contactDetail != null)
                {
                    contact.Details.Remove(contactDetail);
                    SaveChanges();
                }
            }
            else
            {
                throw new ContactDoesNotExistException("Cannot remove details from an inactive or non-existent contact.");
            }
        }


        private void SaveChanges()
        {
            ContactStorageSerialization.SerializeContactList(contacts);

        }

    }
}

//agar user inactive hai toh no contact details