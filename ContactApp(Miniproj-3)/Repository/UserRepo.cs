using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using ContactApp_Miniproj_3_.Exceptions;
using ContactApp_Miniproj_3_.Models;
using ContactApp_Miniproj_3_.Services;


namespace ContactApp_Miniproj_3_.Repository
{
    internal class UserRepo
    {
        //public static readonly User user = new User();
        //crud on users
        //add user
        //view users
        //update users
        //delete users 


        public static List<User> users = new List<User>();

        public UserRepo()
        {
            users = DataStorageSerialization.DeserializeUserList();

        }

        public bool CheckIsAdmin(int adminId)
        {
            foreach (var item in users)
            {
                if (item.UserId == adminId && item.IsAdmin)
                {
                    return true;
                }

            }
            return false;
        }

        public bool CheckIsActive(int userId)
        {
            foreach (var item in users)
            {
                if (item.UserId == userId && item.IsActive)
                {
                    return true;
                }
            }
            return false;
        }

        public User GetById(int id)
        {
            var user = users.Where(x => x.UserId == id).FirstOrDefault();
            return user;
        }


        public void AddUser(User user)
        {
            users.Add(user);
            SaveChanges();
        }

        public void AddContactToUser(int userId, Contact contact)
        {
            var user = users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                if (!user.contacts.Any(c => c.ContactId == contact.ContactId))
                {
                    user.contacts.Add(contact);
                    SaveChanges();
                }
                else
                {
                    throw new ContactIdAlreadyExistsException("Contact with this Id already exists for this user");
                }
            }
            else
            {
                throw new UserDoesNotExistException("User does not exist");
            }
        }




        public void UpdateUser(User updatedUser)
        {
            var existingUser = users.FirstOrDefault(u => u.UserId == updatedUser.UserId);
            if (existingUser == null)
            {
                throw new UserDoesNotExistException("User does not exist");
            }

            // Update user details
            existingUser.FName = updatedUser.FName;
            existingUser.LName = updatedUser.LName;
            existingUser.IsActive = updatedUser.IsActive;
            existingUser.IsAdmin = updatedUser.IsAdmin;

            SaveChanges();
        }

        public void DeleteUser(User deleteUser)
        {
            deleteUser.IsActive = false;
            SaveChanges();
        }

        public void DisplayAllUsers()
        {
            //displaying only active users
            var activeUsers = users.Where((user) => user.IsActive).ToList();
            activeUsers.ForEach((user) => Console.WriteLine(user));

        }


        public void RemoveContactFromUser(int userId, int contactId)
        {
            var user = users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                var contact = user.contacts.FirstOrDefault(c => c.ContactId == contactId);
                if (contact != null)
                {
                    user.contacts.Remove(contact);
                    SaveChanges();
                }
                else
                {
                    throw new ContactDoesNotExistException("Contact not found for this user");
                }
            }
            else
            {
                throw new UserDoesNotExistException("User does not exist");
            }
        }


        private void SaveChanges()
        {
            DataStorageSerialization.SerializeUserList(users);
        }

    }
}
