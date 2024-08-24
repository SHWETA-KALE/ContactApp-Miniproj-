using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ContactApp_Miniproj_3_.Models;

namespace ContactApp_Miniproj_3_.Services
{
    internal class ContactDetailsSerialization
    {
        static string path = @"D:\Assignments\ContactApp(Miniproj-3)\ContactApp(Miniproj-3)\Assets\myContactDetails.json";

        public static void SerializeContactDetailsList(List<Contact_Details> contactDetails)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.WriteLine(JsonSerializer.Serialize(contactDetails));
            }
        }

        public static List<Contact_Details> DeserializeContactDetailsList()
        {
            if (!File.Exists(path))
                return new List<Contact_Details>();

            using (StreamReader sr = new StreamReader(path))
            {
                List<Contact_Details> contact_Details = JsonSerializer.Deserialize<List<Contact_Details>>(sr.ReadToEnd());
                return contact_Details;
            }
        }
    }
}
