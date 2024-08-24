using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using ContactApp_Miniproj_3_.Controller;
using ContactApp_Miniproj_3_.Repository;

namespace ContactApp_Miniproj_3_.Models
{
    internal class MainMenu
    {
        private static readonly UserRepo _userRepo = new UserRepo();
        public static void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("==============================================");
                Console.WriteLine($"1. Login\n" +
                        $"2. Exit\n");
                Console.WriteLine("==============================================");
                Console.WriteLine("Enter your choice: ");
                
                int choice;
               

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
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
                    Login();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.\n");
                    break;


            }
        }

        static void DoTaskContact(int choice)
        {
            switch (choice)
            {
                case 1:
                    ContactController.DisplayContactMenu();
                    break;
                case 2:
                    ContactDetailsController.DisplayContactDetailsMenu();
                    break;
            }
        }

        public static void Login()
        {

            Console.WriteLine("=================Welcome To The Contact App===============");
            Console.WriteLine("Enter User Id: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            //checking if user id exists

            var existingUser = _userRepo.GetById(userId);

            if (existingUser != null)
            {

                //first check whether this user is active 
                if (_userRepo.CheckIsActive(userId))
                {

                    //now user exists so we need to check whether it is admin or staff
                    if (_userRepo.CheckIsAdmin(userId))
                    {
                        //if return true then he can perfom crud on users
                        UserController.DisplayUserMenu();

                    }
                    else
                    {
                        //display contact and contact details menu
                        Console.WriteLine("What do you wish to do?");
                        Console.WriteLine($"1.Work on Contacts\n" +
                            $"2.Work on Contact Details\n");
                
                        try
                        {
                            Console.WriteLine("Enter your choice: ");
                            int choice = Convert.ToInt32(Console.ReadLine());
                            DoTaskContact(choice);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                    }
                }
                else
                {
                    Console.WriteLine("User is InActive");
                }

            }
            else
            {
                Console.WriteLine("User does not exist");
            }
        }

        


    }
}


