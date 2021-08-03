using System;
using System.Net;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace HWIDAuthDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //We get our CPU ID, which is basically a hwid. HWID means a way to identify our pc.
            //Some other methods get your windows install id,
            //C Drive serial numbers and hash them but this is just good for simple use
            string hwid = GetHWID();
            Console.WriteLine("Your HWID = " + hwid);
            //We have our HWID string however we need to hash it.
            //For example if our hwid was 123 that means someone could get into the website and be like "oh look the hwid is 123 so i can just change my hwid in registry so i can have access to this program yay"
            //To prevent this we using ⭐hashing⭐, hashing is basically encrypting our hwid but it can't be reversed to the original hwid.
            //We're going to use the MD5 hash becaus

            string hashedhwid = Hash(hwid);
            Console.WriteLine("Your hashed HWID = " + hashedhwid);
            //We now have our hashedhwid which cannot be reversed from "hackers"


            string hwidlist = new WebClient().DownloadString("https://pastebin.com/raw/jdtT7qYr");



            if (hwidlist.Contains(hashedhwid))
            {
                //Authenticated person
                Console.WriteLine("Welcome back");
            }
            else
            {
                //User's hwid isnt in the system.
                Console.WriteLine("Looks like you aren't welcome here");
            }




            //If you want someone to be able to use your program you have to get them to give you their hashed hwid and you add it to the pastebin :)

        }





        public static string GetHWID()
        {
            //Basically get our ProcessorID. You don't need to understand this code, it just works :p
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            foreach (ManagementObject mo in mbs.Get())
                return mo["ProcessorId"].ToString();

            return null;
        }


        public static string Hash(string input)
        {
            //Our SHA-1 hash method
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                    sb.Append(b.ToString("X2"));
                return sb.ToString();
            }
        }
    }
}
