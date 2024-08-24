using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactApp_Miniproj_3_.Exceptions;
using ContactApp_Miniproj_3_.Models;
using ContactApp_Miniproj_3_.Repository;

namespace ContactApp_Miniproj_3_.Controller
{
    internal class UserController
    {
        private readonly static UserRepo _userRepo = new UserRepo();

        public static void DisplayUserMenu()
        {
            Console.WriteLine("..................................USER MENU....................................");
            while (true)
            {
                Console.WriteLine($"1.Add User\n" +
                    $"2.Update User\n" +
                    $"3.Delete User\n" +
                    $"4.View User Details\n" +
                    $"5.Display All Users\n" +
                    $"6.Logout\n");

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

        static void DoTask(int choice)
        {

            switch (choice)
            {
                case 1:
                    AddUser();
                    break;
                case 2:
                    UpdateUser();
                    break;
                case 3:
                    DeleteUser();
                    break;
                case 4:
                    ViewUserDetails();
                    break;
                case 5:
                    GetAllUsers();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.\n\n");
                    break;
            }
        }

        public static void AddUser()
        {
            Console.WriteLine("Enter user id: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            var existingUser = _userRepo.GetById(userId);
            if (existingUser != null)
            {
                throw new UserIdAlreadyExistsException("User with this Id already exists");
            }
            else
            {
                Console.WriteLine("Enter your First Name: ");
                string fName = Console.ReadLine();

                Console.WriteLine("Enter your Last Name: ");
                string lName = Console.ReadLine();

                Console.WriteLine("Is Admin (true/false): ");
                bool isNewUserAdmin = Convert.ToBoolean(Console.ReadLine());

                Console.WriteLine("Is Active (true/false): ");
                bool isActive = Convert.ToBoolean(Console.ReadLine());

                var newUser = new User(userId, fName, lName, isNewUserAdmin, isActive);

                _userRepo.AddUser(newUser);
                Console.WriteLine("User Added Successfully.");
            }

        }

        public static void UpdateUser()
        {
            Console.WriteLine("Enter user id: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            var existingUser = _userRepo.GetById(userId);

            if (existingUser == null)
            {
                throw new UserDoesNotExistException("User does not exist");
            }

            if (!existingUser.IsActive)
            {
                throw new UserIsInActiveException("User is InActive. You cannot update\n");
            }
            Console.WriteLine("Enter your new First Name: ");
            string fName = Console.ReadLine();
            Console.WriteLine("Enter your new Last Name: ");
            string lName = Console.ReadLine();
            Console.WriteLine("IsAdmin? true/false");
            bool isUpdatedUserAdmin = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("IsActive? true/false");
            bool updatedUserActivity = Convert.ToBoolean(Console.ReadLine());

            existingUser.FName = fName;
            existingUser.LName = lName;
            existingUser.IsActive = updatedUserActivity;
            existingUser.IsAdmin = isUpdatedUserAdmin;

            _userRepo.UpdateUser(existingUser);

            Console.WriteLine("User Updated Successfully!");
        }


        public static void DeleteUser()
        {
            Console.WriteLine("Enter user id: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            var existingUser = _userRepo.GetById(userId);

            if (existingUser == null)
            {
                throw new UserDoesNotExistException("User does not exist");
            }

            if (!existingUser.IsActive)
            {
                throw new UserIsInActiveException("User is InActive. You cannot delete\n");
            }
            _userRepo.DeleteUser(existingUser);
            Console.WriteLine("User deleted successfully.");
        }

        public static void ViewUserDetails()
        {
            Console.WriteLine("Enter user id: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            var existingUser = _userRepo.GetById(userId);

            if (existingUser == null)
            {
                throw new UserDoesNotExistException("User does not exist");
            }

            if (!existingUser.IsActive)
            {
                throw new UserIsInActiveException("User not found(Inactive).\n");
            }
            Console.WriteLine(existingUser);
        }


        public static void GetAllUsers()
        {
            _userRepo.DisplayAllUsers();
        }
    }
}