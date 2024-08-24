using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp_Miniproj_3_.Models
{
    internal class Contact
    {
        [Key]
        public int ContactId { get; set; }

        public string FName { get; set; }
        public string LName { get; set; }

       // public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }

        //public User user { get; set; } //nav prop => if fk is there no need of this

        [ForeignKey("user")]

        public int UserId { get; set; } //acting as a foreign key


        public List<Contact_Details> Details { get; set; } = new List<Contact_Details>();

        public Contact() { }


        public Contact(int id, string fname, string lname, bool isactive)
        {
            ContactId = id;
            FName = fname;
            LName = lname;
            IsActive = isactive;

        }

        public override string ToString()
        {
            return $"ContactId: {ContactId}\n" +
                $"FirstNameOfContact: {FName}\n" +
                $"LastNameOfContact: {LName}\n" +
                $"IsActive: {IsActive}\n";
               
        }

    }
}
