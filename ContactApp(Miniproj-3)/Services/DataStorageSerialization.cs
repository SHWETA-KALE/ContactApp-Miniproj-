using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ContactApp_Miniproj_3_.Models;

namespace ContactApp_Miniproj_3_.Services
{
    internal class DataStorageSerialization
    {
        static string path = @"D:\Assignments\ContactApp(Miniproj-3)\ContactApp(Miniproj-3)\Assets\myUsers.json";

        public static void SerializeUserList(List<User> users)
        {
            using (StreamWriter sw = new StreamWriter(path, false)) {
                sw.WriteLine(JsonSerializer.Serialize(users));
            }
        }

        public static List<User> DeserializeUserList() {
            if (!File.Exists(path))
                return new List<User>();

            using (StreamReader sr = new StreamReader(path))
            {
                List<User> users = JsonSerializer.Deserialize<List<User>>(sr.ReadToEnd());
                return users;
            }
        }
    }
}
