using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp_Miniproj_3_.Exceptions
{
    internal class UserDoesNotExistException:Exception
    {
        public UserDoesNotExistException(string message):base(message) { 
        
        }
    }
}
