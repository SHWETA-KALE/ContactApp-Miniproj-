using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ContactApp_Miniproj_3_.Models;

namespace ContactApp_Miniproj_3_.Services
{
    internal class ContactStorageSerialization
    {
        static string path = @"D:\Assignments\ContactApp(Miniproj-3)\ContactApp(Miniproj-3)\Assets\myContacts.json";
        //writing
        public static void SerializeContactList(List<Contact> contacts)
        { 
            string json = JsonSerializer.Serialize(contacts);
            File.WriteAllText(path, json);
        }

        //reading
        public static List<Contact> DeserializeContactList()
        {

            if (!File.Exists(path))
                return new List<Contact>();

            string json = File.ReadAllText(path);
            if (string.IsNullOrWhiteSpace(json))
                return new List<Contact>();
            return JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
        }
    }
}
