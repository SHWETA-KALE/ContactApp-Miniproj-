using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp_Miniproj_3_.Exceptions
{
    internal class ContactDetailsDoesNotExistException:Exception
    {
        public ContactDetailsDoesNotExistException(string message):base(message) 
        {
            
        }
    }
}
