using ContactApp_Miniproj_3_.Controller;
using ContactApp_Miniproj_3_.Models;
using ContactApp_Miniproj_3_.Repository;
using ContactApp_Miniproj_3_.Services;

namespace ContactApp_Miniproj_3_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // new UserRepo();
            //UserRepo.AddUser(1);
            // UserRepo.UpdateUser(6);
            //UserRepo.DeleteUser(1);
            //UserRepo.ViewUserDetails(1);
            //UserRepo.DisplayAllUsers(1);


            // new ContactRepo();
            //ContactRepo.AddContact(1);
            //ContactRepo.UpdateContact(1);
            // ContactRepo.DeleteContact(1);
            //ContactRepo.ViewContactDetails();
            //ContactRepo.DisplayAllContacts();

            //new ContactDetailsRepo();
            //ContactDetailsRepo.AddContactDetails();
            //ContactDetailsRepo.UpdateContactDetails();
            //ContactDetailsRepo.DeleteContactDetails();
            //ContactDetailsRepo.ViewContactDetails();
            // ContactDetailsRepo.DisplayAllContactDetails();

            try
            {
                MainMenu.DisplayMenu();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            

           
           





        }
    }
}
