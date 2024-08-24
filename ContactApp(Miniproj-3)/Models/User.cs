using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp_Miniproj_3_.Models
{
    internal class User
    {
        [Key]
        public int UserId { get; set; }

        public string FName { get; set; }
        public string LName { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }

        //1 user can have multiple contacts 
        public List<Contact> contacts { get; set; } = new List<Contact>();

        public User() { }

        public User(int userid, string fname, string lname ,bool isadmin,bool isactive)
        {
            UserId = userid;
            FName = fname;
            LName = lname;
            IsAdmin = isadmin;
            IsActive = isactive;
        }

        
        public override string ToString()
        {
            return $"UserId: {UserId}\n" +
                $"FirstName: {FName}\n" +
                $"LastName: {LName}\n" +
                $"IsAdmin: {IsAdmin}\n" +
                $"IsActive: {IsActive}\n";
        }
    }
}
