using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ContactApp_Miniproj_3_.Models
{
    internal class Contact_Details
    {
        [Key]
        public int ContactDetailsId { get; set; }

        //Contact Type
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        //public Contact Contact { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }

        public Contact_Details() { }

        public Contact_Details(int id, string email, string phnNo, int contactid)
        {
            ContactDetailsId = id;
            Email = email;
            PhoneNumber = phnNo;
            ContactId = contactid;
        }

        public override string ToString()
        {
            return $"ContactId: {ContactId}\n" +
                $"ContactDetailsId: {ContactDetailsId}\n" +
                $"Email: {Email}\n" +
                $"PhoneNumber: {PhoneNumber}\n";  
        }


    }
}
