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
    internal class ContactDetailsRepo
    {
        private static List<Contact_Details> contactDetails = new List<Contact_Details>();
        private readonly ContactRepo _contactRepo=new ContactRepo();
        public ContactDetailsRepo()
        {
            contactDetails = ContactDetailsSerialization.DeserializeContactDetailsList();
            
        }

        private bool IsContactActive(int contactId)
        {
            var contact = _contactRepo.GetById(contactId);
            return contact != null && contact.IsActive;
        }
        public bool ContactDetailsExists(int contactDetailsId)
        {
            return contactDetails.Any(cd => cd.ContactDetailsId == contactDetailsId);
        }

        public Contact_Details GetById(int contactDetailsId)
        {
            return contactDetails.FirstOrDefault(cd => cd.ContactDetailsId == contactDetailsId);
        }

        public void AddContactDetails(Contact_Details newContactDetails)
        {

            contactDetails.Add(newContactDetails);
            _contactRepo.AddContactDetailsToContact(newContactDetails.ContactId, newContactDetails);

            SaveChanges();
        }

        public void UpdateContactDetails(int contactDetailsId, string newPhoneNumber, string newEmail)
        {
            var contactDetail = GetById(contactDetailsId);
            if (contactDetail == null)
            {
                throw new ContactDetailsDoesNotExistException("Contact details not found.");
            }

            contactDetail.PhoneNumber = newPhoneNumber;
            contactDetail.Email = newEmail;

            SaveChanges();
        }


        public void DeleteContactDetails(int contactDetailsId)
        { 

            var contactDetail = GetById(contactDetailsId);
            if (contactDetail == null)
            {
                throw new ContactDetailsDoesNotExistException("Contact details not found.");
            }

            _contactRepo.RemoveContactDetailsFromContact(contactDetail.ContactId, contactDetailsId);
            contactDetails.Remove(contactDetail);
            SaveChanges();

        }

        public void DisplayAllContactDetails()
        {
            contactDetails.ForEach(conDetails => Console.WriteLine(conDetails));
        }

        private void SaveChanges()
        {
            ContactDetailsSerialization.SerializeContactDetailsList(contactDetails);
        }

    }
}

