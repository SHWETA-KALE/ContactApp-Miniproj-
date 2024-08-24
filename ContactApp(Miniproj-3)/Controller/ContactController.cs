using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using ContactApp_Miniproj_3_.Exceptions;
using ContactApp_Miniproj_3_.Models;
using ContactApp_Miniproj_3_.Repository;
using ContactApp_Miniproj_3_.Services;

namespace ContactApp_Miniproj_3_.Controller
{
    internal class ContactController
    {
        private static readonly ContactRepo _contactRepo = new ContactRepo();
        private static readonly UserRepo _userRepo = new UserRepo();
        public static void DisplayContactMenu()
        {
            while (true)
            {
                Console.WriteLine("..............................CONTACT MENU....................................");
                Console.WriteLine($"1.Add new Contact\n" +
                    $"2.Modify\n" +
                    $"3.Delete\n" +
                    $"4.Display All Contacts\n" +
                    $"5.Find Contact\n" +
                    $"6.logout");

                int choice;
                try
                {
                    Console.WriteLine("Enter your choice: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("............................................................................................");
                    DoTask(choice);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


            }
        }

        public static void DoTask(int choice)
        {
            switch (choice)
            {
                case 1:
                    AddContact();
                    break;

                case 2:
                    UpdateContact();
                    break;

                case 3:
                    DeleteContact();
                    break;
                case 4:
                    DisplayAllContacts();
                    break;
                case 5:
                    ViewContact(); //this isfind contact
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.\n\n");
                    break;
            }

        }

        public static void AddContact()
        {

            Console.WriteLine("Enter Contact Id: ");
            int contactId = Convert.ToInt32(Console.ReadLine());

            var existingContact = _contactRepo.GetById(contactId);
            if (existingContact != null)
            {
                throw new ContactIdAlreadyExistsException("Contact with this Id already exists");
            }
            else
            {
                Console.WriteLine("Enter your First Name: ");
                string fName = Console.ReadLine();

                Console.WriteLine("Enter your Last Name: ");
                string lName = Console.ReadLine();

                Console.WriteLine("Is Active (true/false): ");
                bool isActive = Convert.ToBoolean(Console.ReadLine());

                var newContact = new Contact(contactId, fName, lName, isActive);

                Console.WriteLine("Enter the user id to associate this contact with: ");
                int userId = Convert.ToInt32(Console.ReadLine());

                var user = _userRepo.GetById(userId);
                if (user == null || !user.IsActive)
                {
                    throw new UserDoesNotExistException("User does not exist or is inactive");
                }
                _contactRepo.AddContact(newContact);

                _userRepo.AddContactToUser(userId, newContact);

                Console.WriteLine("Contact added successfully");
            }


        }

        public static void UpdateContact()
        {
            Console.WriteLine("Enter contact id: ");
            int contactId = Convert.ToInt32(Console.ReadLine());

            var existingContact = _contactRepo.GetById(contactId);
            if (existingContact == null)
            {
                throw new ContactDoesNotExistException("Contact does not exist");
            }

            if (!existingContact.IsActive)
            {
                throw new ContactIsInActiveException("Contact is InActive");
            }

            Console.WriteLine("Enter your new First Name: ");
            string fName = Console.ReadLine();

            Console.WriteLine("Enter your new Last Name: ");
            string lName = Console.ReadLine();

            Console.WriteLine("Is Active (true/false): ");
            bool isActive = Convert.ToBoolean(Console.ReadLine());

            existingContact.FName = fName;
            existingContact.LName = lName;
            existingContact.IsActive = isActive;

            _contactRepo.UpdateContact(existingContact);

            foreach (var user in UserRepo.users)
            {
                var contactToUpdate = user.contacts.FirstOrDefault(c => c.ContactId == contactId);
                if (contactToUpdate != null)
                {
                    contactToUpdate.FName = fName;
                    contactToUpdate.LName = lName;
                    contactToUpdate.IsActive = isActive;
                }
            }

            // Save the updated user list
            DataStorageSerialization.SerializeUserList(UserRepo.users);
            Console.WriteLine("Contact updated successfully");
           

        }
        public static void DeleteContact()
        {
            Console.WriteLine("Enter contact id: ");
            int contactId = Convert.ToInt32(Console.ReadLine());

            var existingContact = _contactRepo.GetById(contactId);
            if (existingContact == null)
            {
                throw new ContactDoesNotExistException("Contact with this Id already exists");
            }

            if (!existingContact.IsActive)
            {
                throw new ContactIsInActiveException("Contact is InActive");
            }

            // Deactivate contact
            existingContact.IsActive = false;
            _contactRepo.UpdateContact(existingContact);

            // Iterate over all users to update the contact status
            foreach (var user in UserRepo.users)
            {
                var contactToUpdate = user.contacts.FirstOrDefault(c => c.ContactId == contactId);
                if (contactToUpdate != null)
                {
                    contactToUpdate.IsActive = false; // Set the contact's status to inactive in the user's contact list
                }
            }

            // Save the updated user list
            DataStorageSerialization.SerializeUserList(UserRepo.users);

            Console.WriteLine("Contact deactivated and status updated in users' contact lists successfully.");


            //Console.WriteLine("Enter the user id to remove this contact from: ");
            //int userId = Convert.ToInt32(Console.ReadLine());


            //var user = _userRepo.GetById(userId);
            //if (user == null || !user.IsActive)
            //{
            //    throw new UserDoesNotExistException("User does not exist or is inactive");
            //}
            //// Remove contact from user's contact list
            //_userRepo.RemoveContactFromUser(userId, contactId);

            //// Deactivate contact
            //existingContact.IsActive = false;
            //_contactRepo.UpdateContact(existingContact);

            //Console.WriteLine("Contact deactivated and removed from user successfully.");

            ////_contactRepo.DeleteContact(existingContact);
            ////Console.WriteLine("Contact deactivated successfully.");
        }

        public static void DisplayAllContacts()
        {
            _contactRepo.DisplayAllContacts();
        }

        public static void ViewContact()
        {
            Console.WriteLine("Enter contact id: ");
            int contactId = Convert.ToInt32(Console.ReadLine());

            var existingContact = _contactRepo.GetById(contactId);
            //var contact = _contactRepo.contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (existingContact == null)
            {
                throw new ContactDoesNotExistException("Contact with this id does not exist");
            }

            if (!existingContact.IsActive)
            {
                throw new ContactIsInActiveException("Contact is InActive");
            }
            Console.WriteLine(existingContact);
        }
    }
}
