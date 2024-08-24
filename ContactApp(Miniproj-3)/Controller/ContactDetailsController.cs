using System;
using System.Collections.Generic;
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
    internal class ContactDetailsController
    {
        
        private readonly static ContactRepo _contactRepo = new ContactRepo();
        private readonly static ContactDetailsRepo _contactDetailsRepo = new ContactDetailsRepo();


        public static void DisplayContactDetailsMenu()
        {
            while (true)
            {
                Console.WriteLine(".............................................CONTACT DETAILS.................................");
                Console.WriteLine($"1.Add ContactDetails\n" +
                    $"2.Update ContactDetails\n" +
                    $"3.Delete ContactDetails\n" +
                    $"4.Display All ContactsDetails\n" +
                    $"5.View Contact Details\n" +
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
                    AddContactDetails();
                    break;
                case 2:
                    UpdateContactDetails();
                    break;
                case 3:
                    DeleteContactDetails();
                    break;
                case 4:
                    DisplayAllContactsDetails();
                    break;
                case 5:
                    ViewContactDetails();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.\n\n");
                    break;

            }
        }


        static void AddContactDetails()
        {
            Console.WriteLine("Enter the contact ID: ");
            int contactId = Convert.ToInt32(Console.ReadLine());

            if (!_contactRepo.CheckIsActive(contactId))
            {
                throw new ContactDoesNotExistException("Contact with this ID does not exist or is inactive.");
            }

            Console.WriteLine("Enter the Contact Details ID: ");
            int contactDetailsId = Convert.ToInt32(Console.ReadLine());

            if (_contactDetailsRepo.GetById(contactDetailsId) != null)
            {
                throw new ContactDetailsIdAlreadyExistException("Contact details with this ID already exist.");

            }

            Console.WriteLine("Enter your Phone Number: ");
            string phnNumber = Console.ReadLine();

            Console.WriteLine("Enter your Email: ");
            string email = Console.ReadLine();

            Contact_Details newContactDetails = new Contact_Details(contactDetailsId, email, phnNumber, contactId);
            _contactDetailsRepo.AddContactDetails(newContactDetails);
            Console.WriteLine("Contact details added and associated successfully.");
        }


        static void UpdateContactDetails()
        {
            Console.WriteLine("Enter the contact ID: ");
            int contactId = Convert.ToInt32(Console.ReadLine());

            // Check if the contact exists
            if (!_contactRepo.ContactExists(contactId))
            {
                throw new ContactDoesNotExistException("Contact with this ID does not exist.");
            }

            //checking if contact is active before adding contact details
            var contactIsActive = _contactRepo.CheckIsActive(contactId);
            if (!contactIsActive)
            {
                throw new ContactIsInActiveException("Contact with this id is in active");
            }
            Console.WriteLine("Enter the Contact Details ID: ");
            int contactDetailsId = Convert.ToInt32(Console.ReadLine());

            var contactDetails = _contactDetailsRepo.GetById(contactDetailsId);
            if (contactDetails == null)
            {
                throw new ContactDetailsDoesNotExistException("Contact details with this ID does not exist.");

            }

            Console.WriteLine("Enter your Phone Number: ");
            string phnNumber = Console.ReadLine();

            Console.WriteLine("Enter your Email: ");
            string email = Console.ReadLine();

            _contactDetailsRepo.UpdateContactDetails(contactDetailsId, phnNumber, email);
            //save the updated contact list
            ContactStorageSerialization.SerializeContactList(ContactRepo.contacts);
            Console.WriteLine("Contact Details updated successfully");

        }

        static void DeleteContactDetails()
        {
            
            Console.WriteLine("Enter the Contact Details ID: ");
            int contactDetailsId = Convert.ToInt32(Console.ReadLine());

            if (_contactDetailsRepo.GetById(contactDetailsId) == null)
            {
                throw new ContactDetailsDoesNotExistException("Contact details with this ID do not exist.");

            }

            _contactDetailsRepo.DeleteContactDetails(contactDetailsId);
            Console.WriteLine("Contact Details deleted successfully");
            ContactStorageSerialization.SerializeContactList(ContactRepo.contacts);
        }

        static void DisplayAllContactsDetails()
        {
            _contactDetailsRepo.DisplayAllContactDetails();
        }

        static void ViewContactDetails()
        {
            Console.WriteLine("Enter the Contact Details ID: ");
            int contactDetailsId = Convert.ToInt32(Console.ReadLine());

            var contactDetails = _contactDetailsRepo.GetById(contactDetailsId);
            if (contactDetails != null)
            {
                Console.WriteLine(contactDetails);
            }
            else
            {
                throw new ContactDetailsDoesNotExistException("Contact details with this ID do not exist.");
            }
        }

    }
}




